﻿using System;
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

        [HttpPost("balance")]
        public decimal CheckBalance([FromBody] CustomerModel IncomingCustomer)
        {
            //Console.WriteLine(IncomingCustomer.AccountNumber);
            return AccountHandler.CheckBalance(IncomingCustomer.AccountNumber);
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
