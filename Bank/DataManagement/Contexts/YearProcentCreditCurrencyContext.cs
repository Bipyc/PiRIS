using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.DataManagement.Contexts
{
    public class YearProcentCreditCurrencyContext : DbContext
    {
        public YearProcentCreditCurrencyContext(DbContextOptions<YearProcentCreditCurrencyContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<YearProcentCreditCurrency> YearProcentCreditCurrencies { get; set; }
    }
}
