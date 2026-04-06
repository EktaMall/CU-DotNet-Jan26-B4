using SmartBank.DTOs;
using SmartBank.Exceptions;
using SmartBank.Helper;
using SmartBank.Models;
using SmartBank.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SmartBank.Exceptions.CustomExceptions;

namespace SmartBank.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        // ================= CREATE ACCOUNT =================
        public async Task<AccountDto> CreateAccountAsync(CreateAccountDto dto, string userId)
        {
            if (dto.InitialDeposit < 1000)
                throw new BadRequestException("Minimum deposit is ₹1000");

            var allAccounts = await _repo.GetAllAsync();
            var userAccounts = allAccounts.Where(a => a.UserId == userId).ToList();

            var account = new Account
            {
                Name = dto.Name,
                Balance = dto.InitialDeposit,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                Status = userAccounts.Count == 0 ? AccountStatus.ACTIVE : AccountStatus.PENDING
            };

            var created = await _repo.CreateAsync(account);

            created.AccountNumber = AccountNumberGenerator.Generate(created.Id);
            await _repo.UpdateAsync(created);

            if (created.Status == AccountStatus.ACTIVE)
            {
                var transaction = new Transaction
                {
                    AccountId = created.Id,
                    Amount = dto.InitialDeposit,
                    Type = "Deposit",
                    CreatedAt = DateTime.UtcNow
                };
                await _repo.AddTransactionAsync(transaction);
            }

            return Map(created);
        }

        // ================= APPROVE / REJECT =================
        public async Task ApproveAccountAsync(int id)
        {
            var account = await _repo.GetByIdAsync(id) ?? throw new NotFoundException("Account not found");
            if (account.Status != AccountStatus.PENDING)
                throw new BadRequestException("Only pending accounts can be approved");

            account.Status = AccountStatus.ACTIVE;
            await _repo.UpdateAsync(account);
        }

        public async Task RejectAccountAsync(int id)
        {
            var account = await _repo.GetByIdAsync(id) ?? throw new NotFoundException("Account not found");
            if (account.Status != AccountStatus.PENDING)
                throw new BadRequestException("Only pending accounts can be rejected");

            account.Status = AccountStatus.REJECTED;
            await _repo.UpdateAsync(account);
        }

        // ================= GET BY ID =================
        public async Task<AccountDto> GetByIdAsync(int id)
        {
            var account = await _repo.GetByIdAsync(id) ?? throw new NotFoundException("Account not found");
            return Map(account);
        }

        // ================= GET BY USER =================
        public async Task<AccountDto> GetByUserIdAsync(string userId)
        {
            var accounts = await _repo.GetAllAsync();
            var account = accounts.FirstOrDefault(a => a.UserId == userId);
            return account == null ? null : Map(account);
        }

        public async Task<List<AccountDto>> GetByUserIdAllAsync(string userId)
        {
            var accounts = await _repo.GetAllAsync();
            return accounts.Where(a => a.UserId == userId).Select(Map).ToList();
        }

        // ================= GET ALL =================
        public async Task<List<AccountDto>> GetAllAsync()
        {
            var accounts = await _repo.GetAllAsync();
            return accounts.ConvertAll(Map);
        }

        // ================= DEPOSIT =================
        public async Task DepositAsync(TransactionDto dto)
        {
            var account = await _repo.GetByIdAsync(dto.AccountId) ?? throw new NotFoundException("Account not found");
            if (account.Status != AccountStatus.ACTIVE)
                throw new BadRequestException("Account not approved yet");
            if (dto.Amount <= 0)
                throw new BadRequestException("Invalid amount");

            account.Balance += dto.Amount;
            await _repo.UpdateAsync(account);

            var transaction = new Transaction
            {
                AccountId = account.Id,
                Amount = dto.Amount,
                Type = "Deposit",
                CreatedAt = DateTime.UtcNow
            };
            await _repo.AddTransactionAsync(transaction);
        }

        // ================= WITHDRAW =================
        public async Task WithdrawAsync(TransactionDto dto)
        {
            var account = await _repo.GetByIdAsync(dto.AccountId) ?? throw new NotFoundException("Account not found");
            if (account.Status != AccountStatus.ACTIVE)
                throw new BadRequestException("Account not approved yet");
            if (dto.Amount <= 0)
                throw new BadRequestException("Invalid amount");
            if (account.Balance - dto.Amount < 1000)
                throw new BadRequestException("Minimum balance violation");

            account.Balance -= dto.Amount;
            await _repo.UpdateAsync(account);

            var transaction = new Transaction
            {
                AccountId = account.Id,
                Amount = dto.Amount,
                Type = "Withdraw",
                CreatedAt = DateTime.UtcNow
            };
            await _repo.AddTransactionAsync(transaction);
        }

        // ================= GET TRANSACTIONS =================
        public async Task<List<Transaction>> GetTransactionsAsync(int accountId)
        {
            return await _repo.GetTransactionsByAccountIdAsync(accountId);
        }

        // ================= MAPPER =================
        private AccountDto Map(Account a) => new AccountDto
        {
            Id = a.Id,
            AccountNumber = a.AccountNumber,
            Name = a.Name,
            Balance = a.Balance,
            Status = a.Status.ToString(),
            UserId = a.UserId
        };
    }
}