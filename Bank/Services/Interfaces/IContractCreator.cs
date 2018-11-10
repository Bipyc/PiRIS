using Bank.Controllers.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Interfaces
{
    public interface IContractCreator
    {
        Bank.Models.Contract CreateContractDeposit(CreateContractDepositInfo createContractDepositInfo);
    }
}
