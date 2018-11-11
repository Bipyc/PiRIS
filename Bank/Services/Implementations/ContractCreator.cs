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

        private ClientContext _clientContext;

        private ContractContext _contractContext;

        private IAccountHelper _accountHelper;

        private IAccountRegistrator _accountRegistrator;

        private ITransactionHelper _transactionHelper;

        public ContractCreator(ContractContext contractContext, DepositContext depositContext, ClientContext clientContext, IAccountHelper accountHelper, IAccountRegistrator accountRegistrator, ITransactionHelper transactionHelper)
        {
            _depositContext = depositContext;
            _accountRegistrator = accountRegistrator;
            _clientContext = clientContext;
            _transactionHelper = transactionHelper;
            _contractContext = contractContext;
            _accountHelper = accountHelper;
        }

        public Models.Contract CreateContractDeposit(CreateContractDepositInfo createContractDepositInfo)
        {
            Client client = _clientContext.Clients.Where(clientInDb => clientInDb.PassportNumber == createContractDepositInfo.PassportNumber).First();

            Deposit deposit = _depositContext.Deposits.Find(createContractDepositInfo.DepositId);

            Models.Contract contract = RegistrateContract(deposit, client, createContractDepositInfo);

            Account mainDepositAccount = null;
            Account depositPercentAccount = null;

            CreateDepositAccounts(contract, deposit, client, createContractDepositInfo.CurrencyId, createContractDepositInfo.StartDate, out mainDepositAccount, out depositPercentAccount);

            MakeInitialTransactions(createContractDepositInfo, mainDepositAccount, depositPercentAccount);

            return contract;
        }

        private void MakeInitialTransactions(CreateContractDepositInfo createContractDepositInfo, Account mainDepositAccount, Account depositPercentAccount)
        {
            decimal amount = createContractDepositInfo.Amount;
            int cashBoxAccountId = _accountHelper.GetConstantAccount(
                       Common.ConstantAccounts.CashBox,
                       (Common.CurrencyType)createContractDepositInfo.CurrencyId
                   ).Id;
            _transactionHelper.AddToDebit(cashBoxAccountId, amount);
            _transactionHelper.TransferFromCreditToCredit(cashBoxAccountId, mainDepositAccount.Id, amount);
        }

        public Models.Contract RegistrateContract(Deposit deposit, Client client, CreateContractDepositInfo createContractDepositInfo)
        {
            Models.Contract contract = new Models.Contract()
            {
                Amount = createContractDepositInfo.Amount,
                ClientId = client.Id,
                DateOfSign = createContractDepositInfo.StartDate,
                DateOfEnd = createContractDepositInfo.EndDate,
                DepositId = deposit.Id,
                CreditId = null,
                CurrencyTypeId = createContractDepositInfo.CurrencyId
            };

            var result = _contractContext.Add(contract);

            _contractContext.SaveChanges();

            return contract;
        }

        private void CreateDepositAccounts(Models.Contract contract, Deposit deposit, Client client, int currencyTypeId, DateTime creationTime, out Account depositAccount, out Account depositPercentAccount)
        {
            depositAccount = _accountRegistrator.RegistrateAccount(AccountType.Deposit, contract.Id, client.Id, currencyTypeId, creationTime, null);
            depositPercentAccount = _accountRegistrator.RegistrateAccount(AccountType.CreditDeposit, contract.Id, client.Id, currencyTypeId, creationTime, null);
        }
    }
}
