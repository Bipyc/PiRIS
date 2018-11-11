using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Controllers.Models;
using Bank.Models;
using Bank.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsManagerController : ControllerBase
    {
        private readonly IContractCreator _contractCreator;

        public ContractsManagerController(IContractCreator contractCreator)
        {
            _contractCreator = contractCreator;
        }

        // POST: api/ContractsManager
        [HttpPost]
        public int Post([FromBody] CreateContractDepositInfo createContractDepositInfo)
        {
            Contract contract = _contractCreator.CreateContractDeposit(createContractDepositInfo);

            return contract.Id;
        }
    }
}
