using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.DataManagement.Contexts
{
    public class ContractContext : DbContext
    {
        public ContractContext(DbContextOptions<ContractContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Contract> Contracts { get; set; }
    }
}
