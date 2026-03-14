using System.Collections.Generic;

namespace FinTrackPro.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}