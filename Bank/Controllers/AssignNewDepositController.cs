﻿using System;
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
    public class AssignNewDepositController : ControllerBase
    {
        private readonly IContractCreator _contractCreator;

        public AssignNewDepositController(IContractCreator contractCreator)
        {
            _contractCreator = contractCreator;
        }

        // POST: api/AssignNewDeposit
        [HttpPost]
        public CreateContractDepositResponse Post([FromBody] CreateContractDepositInfo createContractDepositInfo)
        {
            CreateContractDepositResponse response = _contractCreator.CreateContractDeposit(createContractDepositInfo);

            return response;
        }
    }
}
