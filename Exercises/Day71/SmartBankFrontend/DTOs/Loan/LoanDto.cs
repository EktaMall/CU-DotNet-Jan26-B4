namespace SmartBankFrontend.DTOs.Loan
{
    public class LoanDto
    {
        public int Id { get; set; }
        public int LoanApplicationId { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public decimal PrincipalAmount { get; set; }
        public decimal AnnualInterestRate { get; set; }
        public int TenureMonths { get; set; }
        public decimal EMIAmount { get; set; }
        public decimal TotalRepayableAmount { get; set; }
        public decimal RemainingBalance { get; set; }
        public DateTime ApprovedOn { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
