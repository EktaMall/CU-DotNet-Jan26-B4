using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBank.DTOs;
using SmartBank.Services;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace SmartBank.Controllers
{
    [ApiController]
    [Route("accounts")]
  //  [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _service.CreateAccountAsync(dto, userId);
            return Ok(result);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyAccounts()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var accounts = await _service.GetByUserIdAllAsync(userId);
            return Ok(accounts);
        }

        // NEW: admin can fetch accounts by customer id
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var accounts = await _service.GetByUserIdAllAsync(userId);
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var account = await _service.GetByIdAsync(id);
            return Ok(account);
        }

        [HttpGet("{accountId}/transactions")]
        public async Task<IActionResult> GetTransactions(int accountId)
        {
            var account = await _service.GetByIdAsync(accountId);
            if (account == null) return NotFound("Account not found");

            var transactions = await _service.GetTransactionsAsync(accountId);
            return Ok(transactions);
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionDto dto)
        {
            await _service.DepositAsync(dto);
            return Ok(new
            {
                success = true,
                message = "Deposit successful"
            });
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionDto dto)
        {
            await _service.WithdrawAsync(dto);
            return Ok(new
            {
                success = true,
                message = "Withdrawal successful"
            });
        }

        [HttpPost("credit")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Credit([FromBody] AccountCreditRequestDto dto)
        {
            if (dto == null)
                return BadRequest(new { success = false, message = "Invalid request" });

            if (string.IsNullOrWhiteSpace(dto.CustomerId))
                return BadRequest(new { success = false, message = "CustomerId is required" });

            if (dto.AccountId <= 0)
                return BadRequest(new { success = false, message = "AccountId is required" });

            if (dto.Amount <= 0)
                return BadRequest(new { success = false, message = "Amount must be greater than zero" });

            var account = await _service.GetByIdAsync(dto.AccountId);

            if (account == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Selected account not found"
                });
            }

            if (!string.Equals(account.UserId, dto.CustomerId, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Selected account does not belong to the customer"
                });
            }

            if (!string.Equals(account.Status, "ACTIVE", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Selected account is not active"
                });
            }

            var transaction = new TransactionDto
            {
                AccountId = account.Id,
                Amount = dto.Amount,
                Type = "Deposit",
                Description = dto.Description,
                ReferenceType = dto.ReferenceType ?? "LoanDisbursement",
                ReferenceId = dto.ReferenceId
            };

            await _service.DepositAsync(transaction);

            return Ok(new
            {
                success = true,
                message = "Credit successful"
            });
        }

        [HttpGet("pending")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPendingAccounts()
        {
            var accounts = await _service.GetAllAsync();
            var pending = accounts.Where(a => a.Status == "PENDING").ToList();
            return Ok(pending);
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            if (role == "Admin")
            {
                var allAccounts = await _service.GetAllAsync();
                return Ok(allAccounts);
            }

            var userAccounts = await _service.GetByUserIdAllAsync(userId);
            return Ok(userAccounts);
        }

        [HttpPut("approve/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveAccount(int id)
        {
            await _service.ApproveAccountAsync(id);
            return Ok("Account approved successfully");
        }

        [HttpPut("reject/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejectAccount(int id)
        {
            await _service.RejectAccountAsync(id);
            return Ok("Account rejected successfully");
        }
    }
}