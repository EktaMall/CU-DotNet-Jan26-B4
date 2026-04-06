using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SmartBankFrontend.Models;
using SmartBankFrontend.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;

namespace SmartBankFrontend.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("gatewayClient");
        }

        // ================= REGISTER =================
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _httpClient.DefaultRequestHeaders.Clear();

            var response = await _httpClient.PostAsJsonAsync("auth/register", model);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Registration successful!";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", content);
            return View(model);
        }

        // ================= LOGIN =================
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _httpClient.DefaultRequestHeaders.Clear();

            var response = await _httpClient.PostAsJsonAsync("auth/login", model);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = content;
                return View(model);
            }

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (result == null || string.IsNullOrEmpty(result.Token))
            {
                ViewBag.Error = "Invalid login response";
                return View(model);
            }

            // Store JWT in session for gateway/API calls
            HttpContext.Session.SetString("JWToken", result.Token);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(result.Token);

            var role = jwt.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.Role ||
                c.Type == "role" ||
                c.Type.EndsWith("/role"))?.Value ?? "";

            var email = jwt.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.Email ||
                c.Type == "email" ||
                c.Type.Contains("email"))?.Value ?? "";

            var userId = jwt.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier ||
                c.Type == "sub" ||
                c.Type == "userId" ||
                c.Type.Contains("nameidentifier"))?.Value ?? "";

            var name = jwt.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.Name ||
                c.Type == "name" ||
                c.Type.Contains("name"))?.Value ?? email;

            HttpContext.Session.SetString("UserRole", role);
            HttpContext.Session.SetString("UserEmail", email);
            HttpContext.Session.SetString("UserId", userId);
            HttpContext.Session.SetString("UserName", name ?? "");

            // IMPORTANT: sign in with cookie auth for [Authorize] to work
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name ?? ""),
                new Claim(ClaimTypes.Email, email ?? ""),
                new Claim(ClaimTypes.Role, role ?? ""),
                new Claim(ClaimTypes.NameIdentifier, userId ?? "")
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            // Redirect by role
            return role == "Admin"
                ? RedirectToAction("Index", "AdminDashboard")
                : RedirectToAction("Index", "CustomerDashboard");
        }

        // ================= LOGOUT =================
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}