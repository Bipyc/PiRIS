using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Interfaces
{
    public interface IAccountCreditsManager
    {
        bool AddToAccountViaCashBox(string accountNumber, int currencyTypeId, decimal amount);
    }
}
