using SmartBank.Models;

namespace SmartBank.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> CreateAsync(Account account);
        Task<List<Account>> GetAllAsync();
        Task<Account?> GetByIdAsync(int id);
        Task UpdateAsync(Account account);
        Task AddTransactionAsync(Transaction transaction);
        Task<List<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
        //Task UpdateAsync(Account account);
    }
}
