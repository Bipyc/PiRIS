using Bank.Common;
using Bank.DataManagement.Contexts;
using Bank.Models;
using Bank.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Implementations
{
    public class DepositProcessor : IDepositProcessor
    {
        private ContractContext _contractContext;

        private IAccountHelper _accountHelper;

        private ITransactionHelper _transactionHelper;

        private DepositContext _depositContext;

        public DepositProcessor(ContractContext contractContext, DepositContext depositContext, IAccountHelper accountHelper, ITransactionHelper transactionHelper)
        {
            _contractContext = contractContext;
            _depositContext = depositContext;
            _accountHelper = accountHelper;
            _transactionHelper = transactionHelper;
        }


        public void ProcessDeposits()
        {
            AnalyzeOpenDepositContracts(_contractContext.Contracts.Where(contract => contract.DepositId != null && contract.DateOfEnd <= DateTime.Now).ToList());
        }

        private void AnalyzeOpenDepositContracts(List<Contract> activeDepositContracts)
        {
            foreach (Contract contract in activeDepositContracts)
            {
                Deposit deposit = _depositContext.Deposits
                                                    .Where(dep => dep.Id == contract.DepositId)
                                                    .Include(dep => dep.CurrencyTypes)
                                                    .ThenInclude(currDep => currDep.CurrencyType)
                                                    .First();

                                                    

                DepositTypes depositType = GetDepositType(deposit);

                if (depositType == DepositTypes.Unrevocable)
                {
                    AnalyzeUnrevocableDepositsAccounts(contract, deposit);
                }
                else
                {
                    AnalyzeRevocableDepositsAccounts(contract, deposit);
                }
            }
        }

        private void AnalyzeRevocableDepositsAccounts(Contract contract, Deposit deposit)
        {
            int dateDiff = (int)((DateTime.Now - contract.DateOfEnd).TotalDays);

            if (dateDiff >= 0 && dateDiff % Constants.PeriodOfAddProcentsForRevocableDeposits == 0)
            {
                AddProcentsToDepositContract(contract, deposit);
            }
            if (dateDiff <= 0)
            {
                FinishRevocableDeposit(contract, deposit);
            }
        }

        private void FinishRevocableDeposit(Contract contract, Deposit deposit)
        {
            Common.CurrencyType currencyType = GetCurrencyType(contract.CurrencyTypeId);

            Account bankAccount = _accountHelper.GetConstantAccount(ConstantAccounts.BankAccount, currencyType);

            Account cashBoxAccount = _accountHelper.GetConstantAccount(ConstantAccounts.CashBox, currencyType);

            Account depositAccount = _accountHelper.GetMainAccountForDepositInContract(contract.Id);

            Account creditDepositAccount = _accountHelper.GetCreditAccountForDepositInContract(contract.Id);

            _transactionHelper.TransferFromDebitToCredit(bankAccount.Id, creditDepositAccount.Id, creditDepositAccount.Credit);
            FinishDeposit(contract, bankAccount, cashBoxAccount, depositAccount, creditDepositAccount, creditDepositAccount.Credit);
        }

        private void AddProcentsToDepositContract(Contract contract, Deposit deposit)
        {
            decimal amountProcent = GetAmountMonthlyProcentForRevocableDeposit(deposit, contract.CurrencyTypeId, contract.Amount);

            Common.CurrencyType currencyType = GetCurrencyType(contract.CurrencyTypeId);

            Account bankAccount = _accountHelper.GetConstantAccount(ConstantAccounts.BankAccount, currencyType);

            Account creditDepositAccount = _accountHelper.GetCreditAccountForDepositInContract(contract.Id);

            _transactionHelper.TransferFromDebitToCredit(bankAccount.Id, creditDepositAccount.Id, amountProcent);
        }

        private decimal GetAmountMonthlyProcentForRevocableDeposit(Deposit deposit, int currencyTypeId, decimal amount)
        {
            decimal procent = deposit.CurrencyTypes.Where(currencyType => currencyType.CurrencyType.Id == currencyTypeId).First().Value;

            return (amount / 12) * (procent / 100);
        }

        private void AnalyzeUnrevocableDepositsAccounts(Contract contract, Deposit deposit)
        {
            if ((int)((DateTime.Now - contract.DateOfEnd).TotalDays) <= 0)
            {
                FinishUnrevocableDeposit(contract, deposit);
            }
        }

        private void FinishUnrevocableDeposit(Contract contract, Deposit deposit)
        {
            decimal amountProcent = GetAmountProcentForUnrevocableDeposit(deposit, contract.CurrencyTypeId, contract.Amount);

            Common.CurrencyType currencyType = GetCurrencyType(contract.CurrencyTypeId);

            Account bankAccount = _accountHelper.GetConstantAccount(ConstantAccounts.BankAccount, currencyType);

            Account cashBoxAccount = _accountHelper.GetConstantAccount(ConstantAccounts.CashBox, currencyType);

            Account depositAccount = _accountHelper.GetMainAccountForDepositInContract(contract.Id);

            Account creditDepositAccount = _accountHelper.GetCreditAccountForDepositInContract(contract.Id);

            _transactionHelper.TransferFromDebitToCredit(bankAccount.Id, creditDepositAccount.Id, amountProcent);
            FinishDeposit(contract, bankAccount, cashBoxAccount, depositAccount, creditDepositAccount, amountProcent);
        }

        private void FinishDeposit(Contract contract, Account bankAccount, Account cashBoxAccount, Account depositAccount, Account creditDepositAccount, decimal amountProcent)
        {
            _transactionHelper.TransferFromDebitToDebit(creditDepositAccount.Id, cashBoxAccount.Id, amountProcent);
            _transactionHelper.AddToCredit(cashBoxAccount.Id, -amountProcent);

            _transactionHelper.TransferFromDebitToDebit(depositAccount.Id, cashBoxAccount.Id, contract.Amount);
            _transactionHelper.AddToCredit(cashBoxAccount.Id, -contract.Amount);
        }

        private decimal GetAmountProcentForUnrevocableDeposit(Deposit deposit, int currencyTypeId, decimal initialPrice)
        {
            decimal procent = deposit.CurrencyTypes.Where(currencyType => currencyType.CurrencyType.Id == currencyTypeId).First().Value;

            return initialPrice * (procent / 100);
        }

        private DepositTypes GetDepositType(Deposit deposit)
        {
            return (DepositTypes)Enum.ToObject(typeof(DepositTypes), deposit.Type);
        }

        private Common.CurrencyType GetCurrencyType(int currencyTypeId)
        {
            return (Common.CurrencyType)Enum.ToObject(typeof(Common.CurrencyType), currencyTypeId);
        }
    }
}
