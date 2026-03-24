using SmartBankAccountService.Models;

namespace SmartBankAccountService.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> AddAsync(Account account);
        Task<List<Account>> GetAllAsync();
        Task<Account?> GetByIdAsync(int id);
        Task UpdateAsync(Account account);
        Task SaveChangesAsync();
    }
}