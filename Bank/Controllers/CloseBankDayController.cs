using System;
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

        public CloseBankDayController(IDepositProcessor depositProcessor)
        {
            _depositProcessor = depositProcessor;
        }


        [HttpGet]
        public IActionResult CloseBankDay()
        {
            _depositProcessor.ProcessDeposits();

            return Ok();
        }
    }
}
