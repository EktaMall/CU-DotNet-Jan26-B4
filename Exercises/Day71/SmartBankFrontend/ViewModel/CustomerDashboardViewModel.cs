using SmartBankFrontend.DTOs;

namespace SmartBankFrontend.ViewModel
{
    public class CustomerDashboardViewModel
    {
        public List<AccountDto> Accounts { get; set; } = new();

        // ✅ Selected account
        public int SelectedAccountId { get; set; }
        public AccountDto Account { get; set; }

        // ✅ Transactions for selected account
        public List<TransactionDto> Transactions { get; set; } = new();
    }
}