using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.DataManagement.Contexts
{
    public class CurrencyTypeContext : DbContext
    {
        public CurrencyTypeContext(DbContextOptions<CurrencyTypeContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<CurrencyType> CurrencyTypes { get; set; }
    }
}
