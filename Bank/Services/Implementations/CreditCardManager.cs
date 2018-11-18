using Bank.DataManagement.Contexts;
using Bank.Models;
using Bank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Implementations
{
    public class CreditCardManager : ICreditCardManager
    {
        private ITransactionHelper _transactionHelper;

        private AccountContext _accountContext;

        private IAccountHelper _accountHelper;


        public CreditCardManager(ITransactionHelper transactionHelper, IAccountHelper accountHelper, AccountContext accountContext)
        {
            _transactionHelper = transactionHelper;
            _accountContext = accountContext;
            _accountHelper = accountHelper;
        }


        public Account GetAccountInfo(int accountId, out string error)
        {
            Account account = _accountContext.Accounts.Find(accountId);

            if (account != null)
            {
                error = "";
            }
            else
            {
                error = "Error. Cannot find account by Id" + accountId.ToString();
            }

            return account;
        }

        public bool GetMoneyFromAccount(int accountId, decimal amount, out string error)
        {
            Account account = _accountContext.Accounts.Find(accountId);

            if (account != null)
            {
                if (account.SummaryAmount >= amount)
                {
                    Account cashBoxAccount = _accountHelper
                        .GetConstantAccount(Common.ConstantAccounts.CashBox, (Common.CurrencyType)account.CurrencyTypeId);

                    _transactionHelper.TransferFromCreditToDebit(account.Id, cashBoxAccount.Id, amount);
                    _transactionHelper.AddToCredit(cashBoxAccount.Id, amount);

                    error = "";

                    return true;
                }
                else
                {
                    error = "Error. Not enough money.";

                    return false;
                }
            }
            else
            {
                error = "Error. Cannot find account by Id " + accountId.ToString();

                return false;
            }
        }

        public int LogIn(string accountNumber, string PIN, out string error)
        {
            Account account = _accountHelper.GetAccountByAccountNumber(accountNumber);

            if (account != null)
            {
                if (account.PIN == PIN)
                {
                    error = "";

                    return account.Id;
                }
                else
                {
                    error = "Error. Incorrect PIN";

                    return -1;
                }
            }
            else
            {
                error = "Cannot log in. Account with specified account number doesn''t found";

                return -1;
            }
        }

        public bool TransferFromAccountToAnotherAccount(int accountFromId, string accountNumberToId, decimal amount, out string error)
        {
            Account accountFrom = _accountContext.Accounts.Find(accountFromId);

            if (accountFrom != null)
            {
                Account accountTo = _accountHelper.GetAccountByAccountNumber(accountNumberToId);

                if (accountTo != null)
                {
                    if (accountFrom.SummaryAmount >= amount)
                    {
                        _transactionHelper.TransferFromCreditToDebit(accountFrom.Id, accountTo.Id, amount);

                        error = "";

                        return true;
                    }
                    else
                    {
                        error = "Error. Not enough money";

                        return false;
                    }
                }
                else
                {
                    error = "Error. Cannot find account to by account number " + accountNumberToId;

                    return false;
                }
            }
            else
            {
                error = "Error. Cannot find account from by Id " + accountFromId.ToString();

                return false;
            }
        }
    }
}
