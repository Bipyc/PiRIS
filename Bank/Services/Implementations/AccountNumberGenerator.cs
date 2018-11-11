using Bank.Common;
using Bank.DataManagement.Contexts;
using Bank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Implementations
{
    public class AccountNumberGenerator : IAccountNumberGenerator
    {
        private IDictionary<AccountType, Func<string>> _generateAccountNumberFuncDictionary;

        private ConfigContext _configContext;


        public AccountNumberGenerator(ConfigContext configContext) : this()
        {
            _configContext = configContext;
        }

        public AccountNumberGenerator()
        {
            _generateAccountNumberFuncDictionary = new Dictionary<AccountType, Func<string>>();

            _generateAccountNumberFuncDictionary.Add(AccountType.Normal, GenerateNumberForNormalAccount);
            _generateAccountNumberFuncDictionary.Add(AccountType.Credit, GenerateNumberForCreditAccount);
            _generateAccountNumberFuncDictionary.Add(AccountType.Deposit, GenerateNumberForDepositAccount);
            _generateAccountNumberFuncDictionary.Add(AccountType.CreditDeposit, GenerateNumberForCreditDepositAccount);
        }

        private string GetCurrentValueOfConfig(string name)
        {
            string value = _configContext.Configs.Where(config => config.Name == name).First().Value;

            return value;
        }

        private void SetValueForConfig(string name, string value)
        {
            _configContext.Configs.Where(config => config.Name == name).First().Value = value;

            _configContext.SaveChanges();
        }

        private string GenerateAccountNumber(string configName)
        {
            long value = Convert.ToInt64(GetCurrentValueOfConfig(configName));

            value += 10;

            SetValueForConfig(configName, value.ToString());

            return value.ToString();
        }

        private string GenerateNumberForCreditDepositAccount()
        {
            return GenerateAccountNumber(Constants.CreditDepositAccountNumberConfigName);
        }

        private string GenerateNumberForDepositAccount()
        {
            return GenerateAccountNumber(Constants.DepositAccountNumberConfigName);
        }

        private string GenerateNumberForCreditAccount()
        {
            return GenerateAccountNumber(Constants.ClientsCreditAccountNumberConfigName);
        }

        private string GenerateNumberForNormalAccount()
        {
            return GenerateAccountNumber(Constants.ClientsCurrentAccountNumberConfigName);
        }

        public string GenerateAccountNumber(AccountType accountType) 
        {
            return _generateAccountNumberFuncDictionary[accountType]();
        }
    }
}
