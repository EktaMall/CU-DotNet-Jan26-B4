namespace SmartBankFrontend.DTOs.Loan
{
    public class LoanEmiDto
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public int InstallmentNumber { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaidOn { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
        public string? FailureReason { get; set; }
    }
}