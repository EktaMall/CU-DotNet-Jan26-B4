using Microsoft.AspNetCore.Mvc;
using SmartBankFrontend.DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class AdminDashboardController : Controller
{
    private readonly HttpClient _httpClient;

    public AdminDashboardController(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("gatewayClient");
    }

    // ✅ ROLE PROTECTION
    private bool IsAdmin()
    {
        return HttpContext.Session.GetString("UserRole") == "Admin";
    }

    private string GetToken()
    {
        return HttpContext.Session.GetString("JWToken");
    }

    public IActionResult Index()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        return View();
    }

    public async Task<IActionResult> AllAccounts()
    {
        if (!IsAdmin()) return RedirectToAction("Login", "Account");

        var token = GetToken();

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync("accounts");

        var accounts = response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<List<AccountDto>>()
            : new List<AccountDto>();

        return View(accounts);
    }

    public async Task<IActionResult> PendingAccounts()
    {
        if (!IsAdmin()) return RedirectToAction("Login", "Account");

        var token = GetToken();

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync("accounts/pending");

        var accounts = response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<List<AccountDto>>()
            : new List<AccountDto>();

        return View(accounts);
    }

    public async Task<IActionResult> Approve(int id)
    {
        if (!IsAdmin()) return RedirectToAction("Login", "Account");

        var token = GetToken();

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsync($"accounts/approve/{id}", null);

        var error = await response.Content.ReadAsStringAsync();

        TempData["Message"] = response.IsSuccessStatusCode
            ? "✅ Account approved successfully"
            : $"❌ Failed: {response.StatusCode}";

        return RedirectToAction("PendingAccounts");
    }
    public async Task<IActionResult> Reject(int id)
    {
        if (!IsAdmin()) return RedirectToAction("Login", "Account");

        var token = GetToken();

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsync($"accounts/reject/{id}", null);

        TempData["Message"] = response.IsSuccessStatusCode
            ? "❌ Account rejected"
            : "⚠️ Failed to reject";

        return RedirectToAction("PendingAccounts");
    }
}