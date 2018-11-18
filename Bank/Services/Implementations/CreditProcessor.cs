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
    public class CreditProcessor : ICreditProcessor
    {
        private ContractContext _contractContext;

        private IAccountHelper _accountHelper;

        private ITransactionHelper _transactionHelper;

        private CreditContext _creditContext;


        public CreditProcessor(ContractContext contractContext, CreditContext creditContext, IAccountHelper accountHelper, ITransactionHelper transactionHelper)
        {
            _contractContext = contractContext;
            _creditContext = creditContext;
            _accountHelper = accountHelper;
            _transactionHelper = transactionHelper;
        }

        public void ProcessCredits()
        {
            AnalyzeOpenCreditContracts(_contractContext.Contracts.Where(contract => contract.CreditId != null && contract.DateOfEnd <= DateTime.Now).ToList());
        }

        private void AnalyzeOpenCreditContracts(List<Contract> activeCreditContracts)
        {
            foreach (Contract contract in activeCreditContracts)
            {
                Credit credit = _creditContext.Credits
                                                    .Where(cred => cred.Id == contract.CreditId)
                                                    .Include(cred => cred.CurrencyTypes)
                                                    .ThenInclude(currCred => currCred.CurrencyType)
                                                    .First();



                CreditTypes creditType = GetCreditType(credit);

                if (creditType == CreditTypes.Annuity)
                {
                    AnalyzeAnnuityCreditsAccounts(contract, credit);
                }
                else
                {
                    AnalyzeDifferentiatedCreditsAccounts(contract, credit);
                }
            }
        }

        private void AnalyzeDifferentiatedCreditsAccounts(Contract contract, Credit credit)
        {
            int dateDiff = (int)((DateTime.Now - contract.DateOfEnd).TotalDays);

            if (dateDiff > 0 && dateDiff % Constants.PeriodOfProcessProcentsCredits == 0)
            {
                ProcessDifferenciatedCredit(contract, credit);
            }
        }

        private void ProcessDifferenciatedCredit(Contract contract, Credit credit)
        {
            decimal monthlyPercent = GetAmountMonthlyProcentForCredit(credit, contract.CurrencyTypeId, contract.Amount);

            decimal amount = contract.Amount * (monthlyPercent / 100);

            Common.CurrencyType currencyType = GetCurrencyType(contract.CurrencyTypeId);

            Account bankAccount = _accountHelper.GetConstantAccount(ConstantAccounts.BankAccount, currencyType);

            Account creditPercentsAccount = _accountHelper.GetPercentCreditAccountInContract(contract.Id);

            _transactionHelper.TransferFromCreditToCredit(creditPercentsAccount.Id, bankAccount.Id, amount);
        }

        private void AnalyzeAnnuityCreditsAccounts(Contract contract, Credit credit)
        {
            int dateDiff = (int)((DateTime.Now - contract.DateOfEnd).TotalDays);

            if (dateDiff > 0 && dateDiff % Constants.PeriodOfProcessProcentsCredits == 0)
            {
                ProcessAnnuityCredit(contract, credit);
            }
        }

        private void ProcessAnnuityCredit(Contract contract, Credit credit)
        {
            decimal monthlyPercent = GetAmountMonthlyProcentForCredit(credit, contract.CurrencyTypeId, contract.Amount);

            int countMonth = (int)((contract.DateOfSign - contract.DateOfEnd).TotalDays / 30);

            decimal amount = contract.Amount * (monthlyPercent) / (1 - (decimal)Math.Pow((1 + (double)monthlyPercent), -countMonth));

            Common.CurrencyType currencyType = GetCurrencyType(contract.CurrencyTypeId);

            Account bankAccount = _accountHelper.GetConstantAccount(ConstantAccounts.BankAccount, currencyType);

            Account creditPercentsAccount = _accountHelper.GetPercentCreditAccountInContract(contract.Id);

            _transactionHelper.TransferFromCreditToCredit(creditPercentsAccount.Id, bankAccount.Id, amount);
        }

        private decimal GetAmountMonthlyProcentForCredit(Credit credit, int currencyTypeId, decimal amount)
        {
            decimal procent = credit.CurrencyTypes
                .Where(currencyType => currencyType.CurrencyType.Id == currencyTypeId)
                .First()
                .Value;

            return (amount / 12) * (procent / 100);
        }

        private CreditTypes GetCreditType(Credit credit)
        {
            return (CreditTypes)Enum.ToObject(typeof(CreditTypes), credit.Type);
        }

        private Common.CurrencyType GetCurrencyType(int currencyTypeId)
        {
            return (Common.CurrencyType)Enum.ToObject(typeof(Common.CurrencyType), currencyTypeId);
        }
    }
}
