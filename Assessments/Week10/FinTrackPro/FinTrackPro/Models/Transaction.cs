using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FinTrackPro.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        [ValidateNever]
        public Account Account { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }

        public string Category { get; set; }

        public DateTime Date { get; set; }
    }
}