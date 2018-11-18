using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Common
{
    public static class Constants
    {
        public const string CashBoxAccountNumberBYN = "0101000000011";
        public const string CashBoxAccountNumberRUB = "0101000000021";
        public const string CashBoxAccountNumberUSD = "0101000000031";
        public const string CashBoxAccountNumberEUR = "0101000000041";

        public const string BankAccountNumberBYN = "7327000000011";
        public const string BankAccountNumberRUB = "7327000000021";
        public const string BankAccountNumberUSD = "7327000000031";
        public const string BankAccountNumberEUR = "7327000000041";

        public const string ClientsCreditAccountNumberPrefix = "2400";
        public const string ClientsCurrentAccountNumberPrefix = "3014";
        public const string CashBoxAccountNumberPrefix = "0101";
        public const string BankAccountNumberPrefix = "7327";
        public const string CreditDepositAccountNumberPrefix = "1337";
        public const string DepositAccountNumberPrefix = "1336";

        public const string ClientsCreditAccountNumberConfigName = "LastIdForCreditAccounts";
        public const string ClientsCurrentAccountNumberConfigName = "LastIdForCurrentAccounts";
        public const string CreditDepositAccountNumberConfigName = "LastIdForCreditDepositAccounts";
        public const string DepositAccountNumberConfigName = "LastIdForDepositAccounts";

        public const int PeriodOfAddProcentsForRevocableDeposits = 30;
        public const int PeriodOfProcessProcentsCredits = 30;
    }
}
