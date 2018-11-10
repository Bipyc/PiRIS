using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Controllers.Models
{
    public class CreateContractDepositInfo
    {
        public string PassportNumber { get; set; }
        public int DepositId { get; set; }
        public int CurrencyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
    }
}
