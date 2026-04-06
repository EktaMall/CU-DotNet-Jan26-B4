using SmartBankFrontend.DTOs.Loan;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SmartBankFrontend.Services
{
    public class LoanGatewayService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoanGatewayService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient("gatewayClient");

            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        public async Task<(bool Success, string Message)> ApplyLoanAsync(ApplyLoanDto dto)
        {
            var client = CreateClient();
            var response = await client.PostAsJsonAsync("/loans/apply", dto);
            var message = await response.Content.ReadAsStringAsync();

            return (response.IsSuccessStatusCode, message);
        }
        public async Task<List<LoanApplicationDto>> GetMyApplicationsAsync()
        {
            var client = CreateClient();
            var response = await client.GetAsync("/loans/my-applications");

            if (!response.IsSuccessStatusCode)
                return new List<LoanApplicationDto>();

            return await response.Content.ReadFromJsonAsync<List<LoanApplicationDto>>() ?? new List<LoanApplicationDto>();
        }

        public async Task<List<LoanDto>> GetMyLoansAsync()
        {
            var client = CreateClient();
            var response = await client.GetAsync("/loans/my-loans");

            if (!response.IsSuccessStatusCode)
                return new List<LoanDto>();

            return await response.Content.ReadFromJsonAsync<List<LoanDto>>() ?? new List<LoanDto>();
        }

        public async Task<List<LoanEmiDto>> GetMyEmisAsync()
        {
            var client = CreateClient();
            var response = await client.GetAsync("/loans/my-emis");

            if (!response.IsSuccessStatusCode)
                return new List<LoanEmiDto>();

            return await response.Content.ReadFromJsonAsync<List<LoanEmiDto>>() ?? new List<LoanEmiDto>();
        }

        public async Task<List<LoanSummaryDto>> GetMySummaryAsync()
        {
            var client = CreateClient();
            var response = await client.GetAsync("/loans/my-summary");

            if (!response.IsSuccessStatusCode)
                return new List<LoanSummaryDto>();

            return await response.Content.ReadFromJsonAsync<List<LoanSummaryDto>>() ?? new List<LoanSummaryDto>();
        }

        public async Task<List<LoanApplicationDto>> GetPendingApplicationsAsync()
        {
            var client = CreateClient();
            var response = await client.GetAsync("/loans/admin/pending");

            if (!response.IsSuccessStatusCode)
                return new List<LoanApplicationDto>();

            return await response.Content.ReadFromJsonAsync<List<LoanApplicationDto>>() ?? new List<LoanApplicationDto>();
        }

        public async Task<List<LoanDto>> GetApprovedLoansAsync()
        {
            var client = CreateClient();
            var response = await client.GetAsync("/loans/admin/approved");

            if (!response.IsSuccessStatusCode)
                return new List<LoanDto>();

            return await response.Content.ReadFromJsonAsync<List<LoanDto>>() ?? new List<LoanDto>();
        }

        public async Task<(bool Success, string Message)> ApproveRejectLoanAsync(ApproveRejectLoanDto dto)
        {
            var client = CreateClient();
            var response = await client.PostAsJsonAsync("/loans/admin/approve-reject", dto);
            var message = await response.Content.ReadAsStringAsync();

            return (response.IsSuccessStatusCode, message);
        }

    }
}