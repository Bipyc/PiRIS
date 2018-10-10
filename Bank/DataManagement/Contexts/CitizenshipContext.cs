using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.DataManagement.Contexts
{
    public class CitizenshipContext : DbContext
    {
        public CitizenshipContext(DbContextOptions<CitizenshipContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Citizenship> Citizenships { get; set; }
    }
}
