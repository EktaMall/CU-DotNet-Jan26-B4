using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartBank.Models;

namespace SmartBank.Data
{
    public class SmartBankContext : DbContext
    {
        public SmartBankContext(DbContextOptions<SmartBankContext> options)
            : base(options)
        {
        }

        // ✅ Rename to plural (best practice)
        public DbSet<Account> Accounts { get; set; } = default!;
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ Account configuration (for enum default)
            modelBuilder.Entity<Account>()
                .Property(a => a.Status)
                .HasDefaultValue(AccountStatus.PENDING);

            // ✅ Transaction → Account relationship
            modelBuilder.Entity<Transaction>()
                .HasOne<Account>()
                .WithMany()
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}