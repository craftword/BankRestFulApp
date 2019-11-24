using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DBModels;

namespace BankRestFulApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        // GET: api/Customer
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Customer
        [HttpPost]
        public string Post([FromBody] CustomerModel IncomingCustomer)
        {
            return Create.CreateCustomer(IncomingCustomer);
        }
        // POST: api/Customer/Login

        [HttpPost("login")]
        public string LoginPost([FromBody] LoginModel LoginDetails)
        {
            return Login.CustomerLogin(LoginDetails);
        }
        // POST : api/Customer/balance
        [HttpPost("balance")]
        public decimal CheckBalance([FromBody] CustomerModel IncomingCustomer)
        {
            //Console.WriteLine(IncomingCustomer.AccountNumber);
            return AccountHandler.CheckBalance(IncomingCustomer.AccountNumber);
        }

        // POST : api/Customer/withdraw
        [HttpPost("withdraw")]
        public string Withdraw([FromBody] DepositModel IncomingCustomer)
        {
            CustomerModel acc = AccountHandler.GetCustomer(IncomingCustomer.AccountNumber);
            
            return AccountHandler.Withdraw(IncomingCustomer.Amount, acc.AccountType, Convert.ToDecimal(acc.Balance), Convert.ToInt32(acc.CustomerID), Convert.ToInt32(acc.AccountID));
           
        }

        //POST : api/Customer/deposit
        [HttpPost("deposit")]
        public decimal Deposit([FromBody] DepositModel IncomingCustomer)
        {
            CustomerModel acc = AccountHandler.GetCustomer(IncomingCustomer.AccountNumber);
            return AccountHandler.Deposit(Convert.ToDecimal(acc.Balance), IncomingCustomer.Amount, Convert.ToInt32(acc.CustomerID), Convert.ToInt32(acc.AccountID));

            
        }



        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
