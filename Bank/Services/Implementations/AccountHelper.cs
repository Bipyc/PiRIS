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
    public class AccountHelper : IAccountHelper
    {
        private IDictionary<GetConstantAccountParam, Func<Account>> _getAccountFuncDictionary;

        private AccountContext _accountContext;


        public AccountHelper(AccountContext accountContext) : base()
        {
            _accountContext = accountContext;
        }

        public AccountHelper()
        {
            _getAccountFuncDictionary = new Dictionary<GetConstantAccountParam, Func<Account>>();
            _getAccountFuncDictionary.Add(
                    new GetConstantAccountParam()
                    {
                        ConstantAccount = ConstantAccounts.BankAccount,
                        CurrencyType = Common.CurrencyType.BYN
                    },
                    GetBankAccountBYN
            );

            _getAccountFuncDictionary.Add(
                    new GetConstantAccountParam()
                    {
                        ConstantAccount = ConstantAccounts.BankAccount,
                        CurrencyType = Common.CurrencyType.EUR
                    },
                    GetBankAccountEUR
            );

            _getAccountFuncDictionary.Add(
                    new GetConstantAccountParam()
                    {
                        ConstantAccount = ConstantAccounts.BankAccount,
                        CurrencyType = Common.CurrencyType.RUB
                    },
                    GetBankAccountRUB
            );

            _getAccountFuncDictionary.Add(
                    new GetConstantAccountParam()
                    {
                        ConstantAccount = ConstantAccounts.BankAccount,
                        CurrencyType = Common.CurrencyType.USD
                    },
                    GetBankAccountUSD
            );

            _getAccountFuncDictionary.Add(
                    new GetConstantAccountParam()
                    {
                        ConstantAccount = ConstantAccounts.CashBox,
                        CurrencyType = Common.CurrencyType.BYN
                    },
                    GetCashBoxAccountBYN
            );

            _getAccountFuncDictionary.Add(
                    new GetConstantAccountParam()
                    {
                        ConstantAccount = ConstantAccounts.CashBox,
                        CurrencyType = Common.CurrencyType.EUR
                    },
                    GetCashBoxAccountEUR
            );

            _getAccountFuncDictionary.Add(
                    new GetConstantAccountParam()
                    {
                        ConstantAccount = ConstantAccounts.CashBox,
                        CurrencyType = Common.CurrencyType.RUB
                    },
                    GetCashBoxAccountRUB
            );

            _getAccountFuncDictionary.Add(
                    new GetConstantAccountParam()
                    {
                        ConstantAccount = ConstantAccounts.CashBox,
                        CurrencyType = Common.CurrencyType.USD
                    },
                    GetCashBoxAccountUSD
            );
        }

        private Account GetBankAccountBYN()
        {
            return _accountContext.BankAccountBYN;
        }

        private Account GetBankAccountRUB()
        {
            return _accountContext.BankAccountBYN;
        }

        private Account GetBankAccountUSD()
        {
            return _accountContext.BankAccountBYN;
        }

        private Account GetBankAccountEUR()
        {
            return _accountContext.BankAccountBYN;
        }

        private Account GetCashBoxAccountBYN()
        {
            return _accountContext.CashBoxAccountBYN;
        }

        private Account GetCashBoxAccountRUB()
        {
            return _accountContext.CashBoxAccountBYN;
        }

        private Account GetCashBoxAccountUSD()
        {
            return _accountContext.CashBoxAccountBYN;
        }

        private Account GetCashBoxAccountEUR()
        {
            return _accountContext.CashBoxAccountBYN;
        }

        public Account GetConstantAccount(ConstantAccounts accountType, Common.CurrencyType currencyType)
        {
            return _getAccountFuncDictionary[new GetConstantAccountParam() { ConstantAccount = accountType, CurrencyType = currencyType }]();
        }
    }
}
