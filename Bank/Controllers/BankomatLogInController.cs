using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Controllers.Models.Bankomat.Requests;
using Bank.Controllers.Models.Bankomat.Responses;
using Bank.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankomatLogInController : ControllerBase
    {
        private ICreditCardManager _creditCardManager;

        public BankomatLogInController(ICreditCardManager creditCardManager)
        {
            _creditCardManager = creditCardManager;
        }

        [HttpPost]
        public LogInResponse Post([FromBody] LogInRequest logInRequest)
        {
            string error = "";

            int result = _creditCardManager.LogIn(logInRequest.AccountNumber, logInRequest.PIN, out error);

            return new LogInResponse() { AccountId = result };
        }
    }
}
