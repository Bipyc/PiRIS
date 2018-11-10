using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.DataManagement.Contexts
{
    public class YearProcentDepositCurrencyContext : DbContext
    {
        public YearProcentDepositCurrencyContext(DbContextOptions<YearProcentDepositCurrencyContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var config = modelBuilder.Entity<YearProcentDepositCurrency>();
                
            config.HasOne(yearProcentDepositCurrencies => yearProcentDepositCurrencies.CurrencyType);
        }

        public DbSet<YearProcentDepositCurrency> YearProcentDepositCurrencies { get; set; }
    }
}
