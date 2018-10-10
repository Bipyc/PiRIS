using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.DataManagement.Contexts
{
    public class MaritalStatusContext : DbContext
    {
        public MaritalStatusContext(DbContextOptions<MaritalStatusContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
    }
}
