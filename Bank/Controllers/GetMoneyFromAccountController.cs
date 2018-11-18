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
    public class GetMoneyFromAccountController : ControllerBase
    {
        private ICreditCardManager _creditCardManager;

        public GetMoneyFromAccountController(ICreditCardManager creditCardManager)
        {
            _creditCardManager = creditCardManager;
        }

        [HttpPost]
        public BankomatResponse Post([FromBody] GetMoneyRequest getMoneyRequest)
        {
            string error = "";

            bool result = _creditCardManager.GetMoneyFromAccount(getMoneyRequest.AccountId, getMoneyRequest.Amount, out error);

            return new BankomatResponse() { IsSuccess = result, Details = error };
        }
    }
}
