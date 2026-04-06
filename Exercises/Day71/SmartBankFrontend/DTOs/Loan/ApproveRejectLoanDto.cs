namespace SmartBankFrontend.DTOs.Loan
{
    public class ApproveRejectLoanDto
    {
        public int LoanApplicationId { get; set; }
        public bool IsApproved { get; set; }
        public string? AdminRemarks { get; set; }
    }
}