namespace SmartBank.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }           // Foreign key
        //public Account Account { get; set; }         // Navigation property

        public decimal Amount { get; set; }
        public string Type { get; set; }             // Deposit / Withdraw
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
