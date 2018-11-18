using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Controllers.Models.Bankomat.Requests
{
    public class LogInRequest
    {
        public string AccountNumber { get; set; }
        public string PIN { get; set; }
    }
}
