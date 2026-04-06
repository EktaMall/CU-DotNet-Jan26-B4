using System.Text.Json.Serialization;

namespace SmartBankFrontend.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Type { get; set; } // Deposit / Withdraw
        public decimal Amount { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime Date { get; set; }
        // Optional: just the AccountId, NOT the full Account object
        public int AccountId { get; set; }
    }
}
