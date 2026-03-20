using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LoanManagementSystem02.Models;

namespace LoanManagementSystem02.Data
{
    public class LoanManagementSystem02Context : DbContext
    {
        public LoanManagementSystem02Context (DbContextOptions<LoanManagementSystem02Context> options)
            : base(options)
        {
        }

        public DbSet<LoanManagementSystem02.Models.Loan> Loan { get; set; } = default!;
    }
}
