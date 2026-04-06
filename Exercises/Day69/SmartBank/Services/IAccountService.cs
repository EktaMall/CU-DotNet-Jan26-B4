using System.Collections.Generic;
using System.Threading.Tasks;
using SmartBank.DTOs;
using SmartBank.Models;

namespace SmartBank.Services
{
    public interface IAccountService
    {
        Task<AccountDto> CreateAccountAsync(CreateAccountDto dto, string userId);
        Task<AccountDto> GetByUserIdAsync(string userId);
        Task<List<AccountDto>> GetAllAsync();
        Task<AccountDto> GetByIdAsync(int id);
        Task DepositAsync(TransactionDto dto);
        Task WithdrawAsync(TransactionDto dto);
        Task<List<Transaction>> GetTransactionsAsync(int accountId);
        Task<List<AccountDto>> GetByUserIdAllAsync(string userId);
        //Task UpdateAsync(Account account);
        Task ApproveAccountAsync(int id);
        Task RejectAccountAsync(int id);

    }
}
