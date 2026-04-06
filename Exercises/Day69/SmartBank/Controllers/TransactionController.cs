using Microsoft.AspNetCore.Mvc;
using SmartBank.Models;
using SmartBank.Data;

namespace SmartBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly SmartBankContext _context;

        public TransactionsController(SmartBankContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(Transaction request)
        {
            var account = await _context.Accounts.FindAsync(request.AccountId);

            if (account == null)
                return NotFound("Account not found");

            // ✅ Block if not approved
            if (account.Status != AccountStatus.ACTIVE)
                return BadRequest("Account not approved yet");

            if (request.Type == "Deposit")
            {
                account.Balance += request.Amount;
            }
            else if (request.Type == "Withdraw")
            {
                if (account.Balance < request.Amount)
                    return BadRequest("Insufficient balance");

                account.Balance -= request.Amount;
            }
            else
            {
                return BadRequest("Invalid transaction type");
            }

            _context.Transactions.Add(request);
            await _context.SaveChangesAsync();

            return Ok(request);
        }
    }
}