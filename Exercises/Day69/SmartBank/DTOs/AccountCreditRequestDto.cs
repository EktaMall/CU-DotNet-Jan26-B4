namespace SmartBank.DTOs
{
   
        public class AccountCreditRequestDto
        {
            public string CustomerId { get; set; } = string.Empty;
            public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; } = string.Empty;
            public string? ReferenceType { get; set; }
            public string? ReferenceId { get; set; }
       
    }
}
