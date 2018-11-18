using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Controllers.Models.Bankomat.Requests
{
    public class TransferMoneyToAnotherAccountRequest
    {
        public int AccountId { get; set; }
        public string AccountNumberTo { get; set; }
        public decimal Amount { get; set; }
    }
}
