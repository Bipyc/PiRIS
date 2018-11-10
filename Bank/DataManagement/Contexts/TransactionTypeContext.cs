using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.DataManagement.Contexts
{
    public class TransactionTypeContext : DbContext
    {
        public TransactionTypeContext(DbContextOptions<TransactionTypeContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TransactionType> TransactionTypes { get; set; }
    }
}
