using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Controllers.Models;
using Bank.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddToCreditController : ControllerBase
    {
        private IAccountCreditsManager _accountCreditsManager;


        public AddToCreditController(IAccountCreditsManager accountCreditsManager)
        {
            _accountCreditsManager = accountCreditsManager;
        }

        // POST: api/AddToCredit
        [HttpPost]
        public void Post([FromBody] AddToCreditParam parameters)
        {
            _accountCreditsManager.AddToAccountViaCashBox(parameters.AccountNumber, parameters.CurrencyTypeId, parameters.Amount);
        }
    }
}
