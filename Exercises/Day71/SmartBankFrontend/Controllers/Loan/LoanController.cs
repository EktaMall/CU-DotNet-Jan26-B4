using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBankFrontend.DTOs.Loan;
using SmartBankFrontend.Services;

namespace SmartBankFrontend.Controllers.Loan
{
    [Authorize(Roles = "Customer")]
    public class LoanController : Controller
    {
        private readonly LoanGatewayService _loanService;

        public LoanController(LoanGatewayService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public IActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Apply(ApplyLoanDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _loanService.ApplyLoanAsync(dto);

            if (!result.Success)
            {
                ViewBag.Error = $"Failed to submit loan request: {result.Message}";
                return View(dto);
            }

            TempData["Success"] = "Loan request submitted successfully.";
            return RedirectToAction(nameof(MyApplications));
        }

        [HttpGet]
        public async Task<IActionResult> MyApplications()
        {
            var data = await _loanService.GetMyApplicationsAsync();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> MyLoans()
        {
            var data = await _loanService.GetMyLoansAsync();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> MyEmis()
        {
            var data = await _loanService.GetMyEmisAsync();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> MySummary()
        {
            var data = await _loanService.GetMySummaryAsync();
            return View(data);
        }
    }
}