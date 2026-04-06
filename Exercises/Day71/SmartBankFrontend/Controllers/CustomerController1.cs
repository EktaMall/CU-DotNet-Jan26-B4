using Microsoft.AspNetCore.Mvc;
using SmartBankFrontend.DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using SmartBankFrontend.ViewModel;

public class CustomerDashboardController : Controller
{
    private readonly HttpClient _httpClient;

    public CustomerDashboardController(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("gatewayClient");
    }

    private string GetToken()
    {
        return HttpContext.Session.GetString("JWToken");
    }

    public async Task<IActionResult> Index(int? accountId)
    {
        var token = HttpContext.Session.GetString("JWToken");

        if (string.IsNullOrEmpty(token))
            return RedirectToAction("Login", "Account");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var vm = new CustomerDashboardViewModel();

        // ✅ Get all accounts
        var response = await _httpClient.GetAsync("accounts");

        if (response.IsSuccessStatusCode)
        {
            vm.Accounts = await response.Content.ReadFromJsonAsync<List<AccountDto>>()
                          ?? new List<AccountDto>();
        }

        // ✅ Default selection
        if (accountId == null && vm.Accounts.Any())
        {
            accountId = vm.Accounts.First().Id;
        }

        vm.SelectedAccountId = accountId ?? 0;
        vm.Account = vm.Accounts.FirstOrDefault(a => a.Id == vm.SelectedAccountId);

        // ✅ Get transactions of selected account
        if (vm.SelectedAccountId != 0)
        {
            var txnResponse = await _httpClient.GetAsync($"accounts/{vm.SelectedAccountId}/transactions");

            if (txnResponse.IsSuccessStatusCode)
            {
                vm.Transactions = await txnResponse.Content.ReadFromJsonAsync<List<TransactionDto>>()
                                  ?? new List<TransactionDto>();
            }
        }

        return View(vm);
    }

    // ================= CREATE ACCOUNT =================

    public IActionResult CreateAccount()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount(CreateAccountDto dto)
    {
        var token = GetToken();

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsJsonAsync("accounts", dto);

        if (response.IsSuccessStatusCode)
        {
            var account = await response.Content.ReadFromJsonAsync<AccountDto>();

            TempData["Message"] = account?.Status == "ACTIVE"
                ? "✅ Account created"
                : "⏳ Waiting for approval";

            return RedirectToAction("Index");
        }

        return View(dto);
    }
    public IActionResult Deposit(int id)
    {
        return View(new TransactionDto { AccountId = id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deposit(TransactionDto dto)
    {
        var token = HttpContext.Session.GetString("JWToken");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsJsonAsync("accounts/deposit", dto);

        var error = await response.Content.ReadAsStringAsync();

        TempData["Message"] = response.IsSuccessStatusCode
            ? "✅ Deposit successful"
            : $"❌ {error}";

        return RedirectToAction("Index");
    }
    public IActionResult Withdraw(int id)
    {
        return View(new TransactionDto { AccountId = id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Withdraw(TransactionDto dto)
    {
        var token = HttpContext.Session.GetString("JWToken");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsJsonAsync("accounts/withdraw", dto);

        var error = await response.Content.ReadAsStringAsync();

        TempData["Message"] = response.IsSuccessStatusCode
            ? "✅ Withdrawal successful"
            : $"❌ {error}";

        return RedirectToAction("Index");
    }
}