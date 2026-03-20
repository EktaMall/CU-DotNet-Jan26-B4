using System.ComponentModel.DataAnnotations;

namespace LoanManagementSystem02.Models
{
    public class Loan
    {
        public int Id { get; set; }

        [Required]
        public string BorrowerName { get; set; }

        public decimal Amount { get; set; }

        public int LoanTermMonths { get; set; }

        public bool IsApproved { get; set; }
    }
}
