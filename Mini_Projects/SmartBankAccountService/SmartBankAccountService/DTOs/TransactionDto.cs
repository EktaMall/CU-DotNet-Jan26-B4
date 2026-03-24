using System.ComponentModel.DataAnnotations;

namespace SmartBankAccountService.DTOs
{
    public class TransactionDto
    {
        [Required]
        public int AccountId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }
    }
}