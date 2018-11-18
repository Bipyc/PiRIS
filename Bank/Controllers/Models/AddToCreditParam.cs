using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Controllers.Models
{
    public class AddToCreditParam
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyTypeId { get; set; }
    }
}
