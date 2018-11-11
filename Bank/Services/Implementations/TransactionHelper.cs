using Bank.Common;
using Bank.DataManagement.Contexts;
using Bank.Models;
using Bank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Implementations
{
    public class TransactionHelper : ITransactionHelper
    {
        private TransactionContext _transactionContext;

        private AccountContext _accountContext;

        public TransactionHelper(TransactionContext transactionContext, AccountContext accountContext)
        {
            _transactionContext = transactionContext;
            _accountContext = accountContext;
        }

        public Transaction AddToCredit(int accountId, decimal amount)
        {
            Transaction transaction = new Models.Transaction()
            {
                AccountToId = accountId,
                AccountFromId = accountId,
                DateTime = DateTime.Now,
                Amount = amount,
                TransactionTypeId = (int)TransactionTypes.AddToCredit
            };

            Account account = _accountContext.Accounts.Find(accountId);

            account.Credit += amount;

            _accountContext.SaveChanges();

            _transactionContext.Add(transaction);
            _transactionContext.SaveChanges();

            return transaction;
        }

        public Transaction AddToDebit(int accountId, decimal amount)
        {
            Transaction transaction = new Models.Transaction()
            {
                AccountToId = accountId,
                AccountFromId = accountId,
                DateTime = DateTime.Now,
                Amount = amount,
                TransactionTypeId = (int)TransactionTypes.AddToDebit
            };

            Account account = _accountContext.Accounts.Find(accountId);

            account.Debit += amount;

            _accountContext.SaveChanges();

            _transactionContext.Add(transaction);
            _transactionContext.SaveChanges();

            return transaction;
        }

        public Transaction TransferFromCreditToCredit(int accountIdFrom, int accountIdTo, decimal amount)
        {
            Transaction transaction = new Models.Transaction()
            {
                AccountToId = accountIdTo,
                AccountFromId = accountIdFrom,
                DateTime = DateTime.Now,
                Amount = amount,
                TransactionTypeId = (int)TransactionTypes.FromCreditToCredit
            };

            Account accountFrom = _accountContext.Accounts.Find(accountIdFrom);

            accountFrom.Credit -= amount;

            Account accountTo = _accountContext.Accounts.Find(accountIdTo);

            accountTo.Credit += amount;

            _accountContext.SaveChanges();

            _transactionContext.Add(transaction);
            _transactionContext.SaveChanges();

            return transaction;
        }

        public Transaction TransferFromCreditToDebit(int accountIdFrom, int accountIdTo, decimal amount)
        {
            Transaction transaction = new Models.Transaction()
            {
                AccountToId = accountIdTo,
                AccountFromId = accountIdFrom,
                DateTime = DateTime.Now,
                Amount = amount,
                TransactionTypeId = (int)TransactionTypes.FromCreditToDebit
            };

            Account accountFrom = _accountContext.Accounts.Find(accountIdFrom);

            accountFrom.Credit -= amount;

            Account accountTo = _accountContext.Accounts.Find(accountIdTo);

            accountTo.Debit += amount;

            _accountContext.SaveChanges();

            _transactionContext.Add(transaction);
            _transactionContext.SaveChanges();

            return transaction;
        }

        public Transaction TransferFromDebitToCredit(int accountIdFrom, int accountIdTo, decimal amount)
        {
            Transaction transaction = new Models.Transaction()
            {
                AccountToId = accountIdTo,
                AccountFromId = accountIdFrom,
                DateTime = DateTime.Now,
                Amount = amount,
                TransactionTypeId = (int)TransactionTypes.FromDebitToCredit
            };

            Account accountFrom = _accountContext.Accounts.Find(accountIdFrom);

            accountFrom.Debit -= amount;

            Account accountTo = _accountContext.Accounts.Find(accountIdTo);

            accountTo.Credit += amount;

            _accountContext.SaveChanges();

            _transactionContext.Add(transaction);
            _transactionContext.SaveChanges();

            return transaction;
        }

        public Transaction TransferFromDebitToDebit(int accountIdFrom, int accountIdTo, decimal amount)
        {
            Transaction transaction = new Models.Transaction()
            {
                AccountToId = accountIdTo,
                AccountFromId = accountIdFrom,
                DateTime = DateTime.Now,
                Amount = amount,
                TransactionTypeId = (int)TransactionTypes.FromDebitToDebit
            };

            Account accountFrom = _accountContext.Accounts.Find(accountIdFrom);

            accountFrom.Debit -= amount;

            Account accountTo = _accountContext.Accounts.Find(accountIdTo);

            accountTo.Debit += amount;

            _accountContext.SaveChanges();

            _transactionContext.Add(transaction);
            _transactionContext.SaveChanges();

            return transaction;
        }
    }
}
