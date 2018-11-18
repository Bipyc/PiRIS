using Bank.Controllers.Models;
using Bank.DataManagement.Contexts;
using Bank.Models;
using Bank.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Implementations
{
    public class ContractCreator : IContractCreator
    {
        private DepositContext _depositContext;

        private CreditContext _creditContext;

        private ClientContext _clientContext;

        private ContractContext _contractContext;

        private IAccountHelper _accountHelper;

        private IAccountRegistrator _accountRegistrator;

        private ITransactionHelper _transactionHelper;

        public ContractCreator(ContractContext contractContext, DepositContext depositContext, CreditContext creditContext, ClientContext clientContext, IAccountHelper accountHelper, IAccountRegistrator accountRegistrator, ITransactionHelper transactionHelper)
        {
            _depositContext = depositContext;
            _creditContext = creditContext;
            _accountRegistrator = accountRegistrator;
            _clientContext = clientContext;
            _transactionHelper = transactionHelper;
            _contractContext = contractContext;
            _accountHelper = accountHelper;
        }

        public CreateContractDepositResponse CreateContractDeposit(CreateContractDepositInfo createContractDepositInfo)
        {
            Client client = _clientContext.Clients.Where(clientInDb => clientInDb.PassportNumber == createContractDepositInfo.PassportNumber).First();

            Deposit deposit = _depositContext.Deposits.Find(createContractDepositInfo.DepositId);

            Models.Contract contract = RegistrateContract(deposit, null, client, createContractDepositInfo.Amount, createContractDepositInfo.StartDate, createContractDepositInfo.EndDate, createContractDepositInfo.CurrencyId);

            Account mainDepositAccount = null;
            Account depositPercentAccount = null;

            CreateDepositAccounts(contract, client, createContractDepositInfo.CurrencyId, createContractDepositInfo.StartDate, out mainDepositAccount, out depositPercentAccount);

            MakeInitialDepositTransactions(createContractDepositInfo, mainDepositAccount, depositPercentAccount);

            return new CreateContractDepositResponse() { AccountNumberCreditDeposit = depositPercentAccount.AccountNumber };
        }

        private void MakeInitialDepositTransactions(CreateContractDepositInfo createContractDepositInfo, Account mainDepositAccount, Account depositPercentAccount)
        {
            decimal amount = createContractDepositInfo.Amount;
            int cashBoxAccountId = _accountHelper.GetConstantAccount(
                       Common.ConstantAccounts.CashBox,
                       (Common.CurrencyType)createContractDepositInfo.CurrencyId
                   ).Id;
            _transactionHelper.AddToDebit(cashBoxAccountId, amount);
            _transactionHelper.TransferFromCreditToCredit(cashBoxAccountId, mainDepositAccount.Id, amount);
        }

        private void MakeInitialCreditTransactions(CreateContractCreditInfo createContractCreditInfo, Account mainCreditAccount, Account creditPercentAccount)
        {
            decimal amount = createContractCreditInfo.Amount;
            int cashBoxAccountId = _accountHelper.GetConstantAccount(
                       Common.ConstantAccounts.CashBox,
                       (Common.CurrencyType)createContractCreditInfo.CurrencyId
                   ).Id;
            int bankAccountId = _accountHelper.GetConstantAccount(
                       Common.ConstantAccounts.BankAccount,
                       (Common.CurrencyType)createContractCreditInfo.CurrencyId
                   ).Id;

            _transactionHelper.TransferFromDebitToDebit(bankAccountId, mainCreditAccount.Id, amount);
        }

        public Models.Contract RegistrateContract(Deposit deposit, Credit credit, Client client, decimal amount, DateTime startDate, DateTime endDate, int currencyId)
        {
            Models.Contract contract = new Models.Contract()
            {
                Amount = amount,
                ClientId = client.Id,
                DateOfSign = startDate,
                DateOfEnd = endDate,
                DepositId = deposit != null ? deposit.Id : (int?)null,
                CreditId = credit != null ? credit.Id : (int?)null,
                CurrencyTypeId = currencyId
            };

            var result = _contractContext.Add(contract);

            _contractContext.SaveChanges();

            return contract;
        }

        private void CreateDepositAccounts(Models.Contract contract, Client client, int currencyTypeId, DateTime creationTime, out Account depositAccount, out Account depositPercentAccount)
        {
            depositAccount = _accountRegistrator.RegistrateAccount(AccountType.Deposit, contract.Id, client.Id, currencyTypeId, creationTime, null);
            depositPercentAccount = _accountRegistrator.RegistrateAccount(AccountType.CreditDeposit, contract.Id, client.Id, currencyTypeId, creationTime, null);
        }

        private void CreateCreditAccounts(Models.Contract contract, Client client, int currencyTypeId, DateTime creationTime, out Account creditAccount, out Account creditPercentAccount)
        {
            creditAccount = _accountRegistrator.RegistrateAccount(AccountType.Normal, contract.Id, client.Id, currencyTypeId, creationTime, null);
            creditPercentAccount = _accountRegistrator.RegistrateAccount(AccountType.Credit, contract.Id, client.Id, currencyTypeId, creationTime, null);
        }

        public CreateContractCreditResponse CreateContractCredit(CreateContractCreditInfo createContractCreditInfo)
        {
            Client client = _clientContext.Clients.Where(clientInDb => clientInDb.PassportNumber == createContractCreditInfo.PassportNumber).First();

            Credit credit = _creditContext.Credits.Find(createContractCreditInfo.CreditId);

            Models.Contract contract = RegistrateContract(null, credit, client, createContractCreditInfo.Amount, createContractCreditInfo.StartDate, createContractCreditInfo.EndDate, createContractCreditInfo.CurrencyId);

            Account mainCreditAccount = null;
            Account creditPercentAccount = null;

            CreateCreditAccounts(contract, client, createContractCreditInfo.CurrencyId, createContractCreditInfo.StartDate, out mainCreditAccount, out creditPercentAccount);

            MakeInitialCreditTransactions(createContractCreditInfo, mainCreditAccount, creditPercentAccount);

            return new CreateContractCreditResponse() { AccountNumberCreditsMain = mainCreditAccount.AccountNumber, AccountNumberCreditsPercent = creditPercentAccount.AccountNumber};
        }
    }
}
