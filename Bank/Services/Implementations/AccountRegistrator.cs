using Bank.DataManagement.Contexts;
using Bank.Models;
using Bank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Implementations
{
    public class AccountRegistrator : IAccountRegistrator
    {
        private AccountContext _accountContext;

        private IAccountNumberGenerator _accountNumberGenerator;


        public AccountRegistrator(AccountContext accountContext, IAccountNumberGenerator accountNumberGenerator)
        {
            _accountContext = accountContext;
            _accountNumberGenerator = accountNumberGenerator;
        }

        public Account RegistrateAccount(AccountType accountType, int contractId, int clientId, int currencyTypeId, DateTime creationTime, string accountName)
        {
            string generatedAccountNumber = _accountNumberGenerator.GenerateAccountNumber(accountType);

            Account account = new Account()
            {
                AccountNumber = generatedAccountNumber,
                ContractId = contractId,
                ClientId = clientId,
                CurrencyTypeId = currencyTypeId,
                AccountName = accountName ?? generatedAccountNumber,
                Debit = 0,
                Credit = 0,
                Saldo = 0,
                CreationDate = creationTime,
                IsClosed = false,
            };

            _accountContext.Add(account);
            _accountContext.SaveChanges();

            return account;
        }
    }
}
