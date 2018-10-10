using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.DataManagement.Contexts
{
    public class DisabilityContext : DbContext
    {
        public DisabilityContext(DbContextOptions<DisabilityContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Disability> Disabilities { get; set; }
    }
}
