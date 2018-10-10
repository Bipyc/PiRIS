using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.DataManagement.Contexts
{
    public class CityContext : DbContext
    {
        public CityContext(DbContextOptions<CityContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<City> Cities { get; set; }
    }
}
