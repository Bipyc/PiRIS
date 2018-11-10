using Bank.Common;
using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.DataManagement.Contexts
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Account> Accounts { get; set; }

        public Account BankAccountBYN
        {
            get
            {
                return Accounts.Where(account => account.AccountNumber == Constants.BankAccountNumberBYN).First();
            }
        }

        public Account CashBoxAccountBYN
        {
            get
            {
                return Accounts.Where(account => account.AccountNumber == Constants.CashBoxAccountNumberBYN).First();
            }
        }

        public Account CashBoxAccountRUB
        {
            get
            {
                return Accounts.Where(account => account.AccountNumber == Constants.CashBoxAccountNumberRUB).First();
            }
        }

        public Account CashBoxAccountUSD
        {
            get
            {
                return Accounts.Where(account => account.AccountNumber == Constants.CashBoxAccountNumberUSD).First();
            }
        }

        public Account CashBoxAccountEUR
        {
            get
            {
                return Accounts.Where(account => account.AccountNumber == Constants.CashBoxAccountNumberEUR).First();
            }
        }
    }
}
