﻿using Bank.Common;
using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Interfaces
{
    public interface IAccountHelper
    {
        Account GetConstantAccount(ConstantAccounts accountType, Common.CurrencyType currencyType);
    }
}
