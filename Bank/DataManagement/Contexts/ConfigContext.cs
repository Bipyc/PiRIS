using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.DataManagement.Contexts
{
    public class ConfigContext : DbContext
    {
        public ConfigContext(DbContextOptions<ConfigContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Config> Configs { get; set; }
    }
}
