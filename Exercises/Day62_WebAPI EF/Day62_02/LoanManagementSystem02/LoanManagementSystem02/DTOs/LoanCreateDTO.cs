namespace LoanManagementSystem02.DTOs
{
    public class LoanCreateDTO
    {
        public string BorrowerName { get; set; }
        public decimal Amount { get; set; }
        public int LoanTermMonths { get; set; }
    }
}
