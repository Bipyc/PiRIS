using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.DataManagement.Contexts
{
    public class DepositContext : DbContext
    {
        public DepositContext(DbContextOptions<DepositContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deposit>()
                .HasMany(deposit => deposit.CurrencyTypes);
        }

        public DbSet<Deposit> Deposits { get; set; }
    }
}
