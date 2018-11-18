using Bank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Implementations
{
    public class AccountCreditsManager : IAccountCreditsManager
    {
        private ITransactionHelper _transactionHelper;

        private IAccountHelper _accountHelper;

        public AccountCreditsManager(ITransactionHelper transactionHelper, IAccountHelper accountHelper)
        {
            _transactionHelper = transactionHelper;
            _accountHelper = accountHelper;
        }

        public bool AddToAccountViaCashBox(string accountNumber, int currencyTypeId, decimal amount)
        {
            int cashBoxAccountId = _accountHelper.GetConstantAccount(
                       Common.ConstantAccounts.CashBox,
                       (Common.CurrencyType)currencyTypeId
                   ).Id;

            int accountNumberId = _accountHelper.GetAccountByAccountNumber(accountNumber).Id;

            _transactionHelper.AddToDebit(cashBoxAccountId, amount);
            _transactionHelper.TransferFromCreditToDebit(cashBoxAccountId, accountNumberId, amount);

            return true;
        }
    }
}
