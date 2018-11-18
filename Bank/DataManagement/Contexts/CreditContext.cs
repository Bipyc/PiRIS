using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.DataManagement.Contexts
{
    public class CreditContext : DbContext
    {
        public CreditContext(DbContextOptions<CreditContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Credit> Credits { get; set; }
    }
}
