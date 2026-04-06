using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBankFrontend.DTOs.Loan;
using SmartBankFrontend.Services;

namespace SmartBankFrontend.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminLoanController : Controller
    {
        private readonly LoanGatewayService _loanService;

        public AdminLoanController(LoanGatewayService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<IActionResult> Pending()
        {
            var data = await _loanService.GetPendingApplicationsAsync();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int loanApplicationId, string? adminRemarks)
        {
            var result = await _loanService.ApproveRejectLoanAsync(new ApproveRejectLoanDto
            {
                LoanApplicationId = loanApplicationId,
                IsApproved = true,
                AdminRemarks = adminRemarks
            });

            TempData["Success"] = result.Success
                ? "Loan approved successfully."
                : $"Failed to approve loan: {result.Message}";

            return RedirectToAction(nameof(Pending));
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int loanApplicationId, string? adminRemarks)
        {
            var result = await _loanService.ApproveRejectLoanAsync(new ApproveRejectLoanDto
            {
                LoanApplicationId = loanApplicationId,
                IsApproved = false,
                AdminRemarks = adminRemarks
            });

            TempData["Success"] = result.Success
                ? "Loan rejected successfully."
                : $"Failed to reject loan: {result.Message}";

            return RedirectToAction(nameof(Pending));
        }

        [HttpGet]
        public async Task<IActionResult> Approved()
        {
            var data = await _loanService.GetApprovedLoansAsync();
            return View(data);
        }
    }
}