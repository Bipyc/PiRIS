using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Common
{
    public enum TransactionTypes
    {
        FromDebitToDebit = 1,
        FromDebitToCredit = 2,
        FromCreditToCredit = 3,
        FromCreditToDebit = 4,
        AddToDebit = 5,
        AddToCredit = 6
    }
}
