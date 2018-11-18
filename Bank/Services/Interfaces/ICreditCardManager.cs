using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Interfaces
{
    public interface ICreditCardManager
    {
        int LogIn(string accountNumber, string PIN, out string error);
        Account GetAccountInfo(int accountId, out string error);
        bool GetMoneyFromAccount(int accountId, decimal amount, out string error);
        bool TransferFromAccountToAnotherAccount(int accountFromId, string accountNumberToId, decimal amount, out string error);
    }
}
