namespace SmartBank.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string? AccountNumber { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public AccountStatus Status { get; set; } = AccountStatus.PENDING; // ACTIVE | PENDING | REJECTED

        // Navigation property for transactions
        //public List<Transaction> Transactions { get; set; } = new();
    }
}