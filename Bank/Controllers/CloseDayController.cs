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
    public class CloseDayController : ControllerBase
    {
        private IDepositProcessor _depositProcessor;

        private ICreditProcessor _creditProcessor;

        public CloseDayController(IDepositProcessor depositProcessor, ICreditProcessor creditProcessor)
        {
            _creditProcessor = creditProcessor;
            _depositProcessor = depositProcessor;
        }

        // GET: api/CloseDay
        [HttpGet]
        public IActionResult CloseDay()
        {
            _creditProcessor.ProcessCredits();
            _depositProcessor.ProcessDeposits();

            return Ok();
        }       
    }
}
