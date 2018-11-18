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
    public class AssignNewCreditController : ControllerBase
    {
        private readonly IContractCreator _contractCreator;

        public AssignNewCreditController(IContractCreator contractCreator)
        {
            _contractCreator = contractCreator;
        }

        // POST: api/AssignNewDeposit
        [HttpPost]
        public CreateContractCreditResponse Post([FromBody] CreateContractCreditInfo createContractCreditInfo)
        {
            CreateContractCreditResponse response = _contractCreator.CreateContractCredit(createContractCreditInfo);

            return response;
        }
    }
}
