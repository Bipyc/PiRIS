﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CloseBankDayController : ControllerBase
    {
        private IDepositProcessor _depositProcessor;

        private ICreditProcessor _creditProcessor;

        public CloseBankDayController(IDepositProcessor depositProcessor, ICreditProcessor creditProcessor)
        {
            _creditProcessor = creditProcessor;
            _depositProcessor = depositProcessor;
        }

        // POST: api/CloseBankDay
        [HttpGet]
        public void CloseBankDay()
        {
            _creditProcessor.ProcessCredits();
            _depositProcessor.ProcessDeposits();
        }

    }
}
