namespace SmartBankFrontend.DTOs.Loan
{
    public class LoanApplicationDto
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public decimal RequestedAmount { get; set; }
        public int TenureMonths { get; set; }
        public decimal AnnualInterestRate { get; set; }
        public string Purpose { get; set; } = string.Empty;
        public decimal MonthlyIncome { get; set; }
        public string EmploymentStatus { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime AppliedOn { get; set; }
        public string? AdminRemarks { get; set; }
    }
}