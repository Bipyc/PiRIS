using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.DataManagement.Contexts
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Client> Clients { get; set; }
    }
}
