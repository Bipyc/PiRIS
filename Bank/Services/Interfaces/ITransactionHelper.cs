using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Bank.Services.Interfaces
{
    public interface ITransactionHelper
    {
        Transaction TransferFromDebitToDebit(int accountIdFrom, int accountIdTo, decimal amount);
        Transaction TransferFromDebitToCredit(int accountIdFrom, int accountIdTo, decimal amount);
        Transaction TransferFromCreditToCredit(int accountIdFrom, int accountIdTo, decimal amount);
        Transaction TransferFromCreditToDebit(int accountIdFrom, int accountIdTo, decimal amount);
        Transaction AddToDebit(int accountId, decimal amount);
        Transaction AddToCredit(int accountid, decimal amount);
    }
}
