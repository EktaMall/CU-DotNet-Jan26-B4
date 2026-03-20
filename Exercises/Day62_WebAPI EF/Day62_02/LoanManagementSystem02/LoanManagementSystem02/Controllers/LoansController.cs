using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoanManagementSystem02.Data;
using LoanManagementSystem02.Models;
using LoanManagementSystem02.DTOs;

namespace LoanManagementSystem02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly LoanManagementSystem02Context _context;

        public LoansController(LoanManagementSystem02Context context)
        {
            _context = context;
        }

        // GET: api/Loans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanReadDTO>>> GetLoan()
        {
            return await _context.Loan
                .Select(l => new LoanReadDTO
                {
                    Id = l.Id,
                    BorrowerName = l.BorrowerName,
                    Amount = l.Amount,
                    LoanTermMonths = l.LoanTermMonths,
                    IsApproved = l.IsApproved
                }).ToListAsync();
        }

        // GET: api/Loans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanReadDTO>> GetLoan(int id)
        {
            var loan = await _context.Loan.FindAsync(id);

            if (loan == null)
                return NotFound();

            return new LoanReadDTO
            {
                Id = loan.Id,
                BorrowerName = loan.BorrowerName,
                Amount = loan.Amount,
                LoanTermMonths = loan.LoanTermMonths,
                IsApproved = loan.IsApproved
            };
        }

        // POST: api/Loans
        [HttpPost]
        public async Task<ActionResult<LoanReadDTO>> PostLoan(LoanCreateDTO dto)
        {
            var loan = new Loan
            {
                BorrowerName = dto.BorrowerName,
                Amount = dto.Amount,
                LoanTermMonths = dto.LoanTermMonths,
                IsApproved = false
            };

            _context.Loan.Add(loan);
            await _context.SaveChangesAsync();

            var result = new LoanReadDTO
            {
                Id = loan.Id,
                BorrowerName = loan.BorrowerName,
                Amount = loan.Amount,
                LoanTermMonths = loan.LoanTermMonths,
                IsApproved = loan.IsApproved
            };

            return CreatedAtAction(nameof(GetLoan), new { id = loan.Id }, result);
        }

        // PUT: api/Loans/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoan(int id, LoanUpdateDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var loan = await _context.Loan.FindAsync(id);

            if (loan == null)
                return NotFound();

            loan.BorrowerName = dto.BorrowerName;
            loan.Amount = dto.Amount;
            loan.LoanTermMonths = dto.LoanTermMonths;
            loan.IsApproved = dto.IsApproved;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Loans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var loan = await _context.Loan.FindAsync(id);

            if (loan == null)
                return NotFound();

            _context.Loan.Remove(loan);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}