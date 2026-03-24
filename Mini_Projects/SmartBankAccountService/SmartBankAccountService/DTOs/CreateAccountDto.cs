using System.ComponentModel.DataAnnotations;

namespace SmartBankAccountService.DTOs
{
    public class CreateAccountDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(1000, double.MaxValue, ErrorMessage = "Initial deposit must be at least 1000")]
        public decimal InitialDeposit { get; set; }
    }
}