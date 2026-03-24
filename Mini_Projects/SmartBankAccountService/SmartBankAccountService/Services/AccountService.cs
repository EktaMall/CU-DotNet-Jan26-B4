using AutoMapper;
using SmartBankAccountService.DTOs;
using SmartBankAccountService.Exceptions;
using SmartBankAccountService.Helpers;
using SmartBankAccountService.Models;
using SmartBankAccountService.Repositories;
using SmartBankAccountService.Services;

namespace SmartBankAccountService.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;
        private const decimal MinimumBalance = 1000m;

        public AccountService(IAccountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AccountDto> CreateAccountAsync(CreateAccountDto dto)
        {
            if (dto.InitialDeposit < MinimumBalance)
            {
                throw new BadRequestException("Minimum initial deposit is 1000");
            }

            var account = new Account
            {
                Name = dto.Name,
                Balance = dto.InitialDeposit,
                CreatedAt = DateTime.UtcNow,
                AccountNumber = string.Empty
            };

            await _repository.AddAsync(account);

            account.AccountNumber = AccountNumberGenerator.Generate(account.Id);
            await _repository.UpdateAsync(account);

            return _mapper.Map<AccountDto>(account);
        }

        public async Task<List<AccountDto>> GetAllAccountsAsync()
        {
            var accounts = await _repository.GetAllAsync();
            return _mapper.Map<List<AccountDto>>(accounts);
        }

        public async Task<AccountDto> GetAccountByIdAsync(int id)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account == null)
            {
                throw new NotFoundException($"Account with Id {id} not found");
            }

            return _mapper.Map<AccountDto>(account);
        }

        public async Task<AccountDto> DepositAsync(TransactionDto dto)
        {
            if (dto.Amount <= 0)
            {
                throw new BadRequestException("Deposit amount must be greater than 0");
            }

            var account = await _repository.GetByIdAsync(dto.AccountId);
            if (account == null)
            {
                throw new NotFoundException($"Account with Id {dto.AccountId} not found");
            }

            account.Balance += dto.Amount;
            await _repository.UpdateAsync(account);

            return _mapper.Map<AccountDto>(account);
        }

        public async Task<AccountDto> WithdrawAsync(TransactionDto dto)
        {
            if (dto.Amount <= 0)
            {
                throw new BadRequestException("Withdrawal amount must be greater than 0");
            }

            var account = await _repository.GetByIdAsync(dto.AccountId);
            if (account == null)
            {
                throw new NotFoundException($"Account with Id {dto.AccountId} not found");
            }

            if (account.Balance - dto.Amount < MinimumBalance)
            {
                throw new BadRequestException("Minimum balance of 1000 must be maintained");
            }

            account.Balance -= dto.Amount;
            await _repository.UpdateAsync(account);

            return _mapper.Map<AccountDto>(account);
        }
    }
}