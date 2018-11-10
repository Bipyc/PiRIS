using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Interfaces
{
    public interface IAccountRegistrator
    {
        Account RegistrateAccount(AccountType accountType, int contractId, int clientId, int currencyTypeId, DateTime creationTime, string accountName);
    }
}
