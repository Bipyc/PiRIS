using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Controllers.Models.Bankomat.Responses
{
    public class BankomatResponse
    {
        public bool IsSuccess { get; set; }
        public string Details { get; set; }
    }
}
