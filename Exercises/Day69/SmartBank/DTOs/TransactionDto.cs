namespace SmartBank.DTOs
{
    public class TransactionDto
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    

            public string Type { get; set; } = string.Empty;   // Deposit / Withdraw
            public string? Description { get; set; }
            public string? ReferenceType { get; set; }         // LoanDisbursement / EMI / Manual
            public string? ReferenceId { get; set; }
        }
    }

