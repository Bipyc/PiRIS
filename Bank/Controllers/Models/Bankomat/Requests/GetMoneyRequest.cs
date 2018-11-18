using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Controllers.Models.Bankomat.Requests
{
    public class GetMoneyRequest
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
