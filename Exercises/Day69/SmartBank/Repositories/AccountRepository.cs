using Microsoft.EntityFrameworkCore;
using SmartBank.Data;
using SmartBank.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBank.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SmartBankContext _context;

        public AccountRepository(SmartBankContext context)
        {
            _context = context;
        }

        // ================= ACCOUNT =================
        public async Task<Account> CreateAsync(Account account)
        {
            _context.Accounts.Add(account); // Correct DbSet name
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            // Fetch account only, no Include (no navigation property)
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Account>> GetAllAsync()
        {
            // Fetch all accounts, no Include
            return await _context.Accounts.ToListAsync();
        }

        public async Task UpdateAsync(Account account)
        {
            _context.Accounts.Update(account); // Correct DbSet
            await _context.SaveChangesAsync();
        }

        // ================= TRANSACTION =================
        public async Task AddTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
        {
            return await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.CreatedAt) // use Date instead of CreatedAt
                .ToListAsync();
        }
    }
}