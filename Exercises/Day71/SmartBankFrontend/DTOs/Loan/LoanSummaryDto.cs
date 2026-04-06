namespace SmartBankFrontend.DTOs.Loan
{
    public class LoanSummaryDto
    {
        public int LoanId { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal EMIAmount { get; set; }
        public decimal TotalRepayableAmount { get; set; }
        public decimal RemainingBalance { get; set; }
        public int TotalEmis { get; set; }
        public int PaidEmis { get; set; }
        public int PendingEmis { get; set; }
        public DateTime? NextDueDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}