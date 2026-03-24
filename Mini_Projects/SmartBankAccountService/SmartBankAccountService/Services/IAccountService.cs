using SmartBankAccountService.DTOs;

namespace SmartBankAccountService.Services
{
    public interface IAccountService
    {
        Task<AccountDto> CreateAccountAsync(CreateAccountDto dto);
        Task<List<AccountDto>> GetAllAccountsAsync();
        Task<AccountDto> GetAccountByIdAsync(int id);
        Task<AccountDto> DepositAsync(TransactionDto dto);
        Task<AccountDto> WithdrawAsync(TransactionDto dto);
    }
}