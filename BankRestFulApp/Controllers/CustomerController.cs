using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DBModels;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
            int result = AccountHandler.Withdraw(IncomingCustomer.Amount, acc.AccountType, Convert.ToDecimal(acc.Balance), Convert.ToInt32(acc.CustomerID), Convert.ToInt32(acc.AccountID));
            if(result == 1)
            {
                return String.Format("Withdraw was successful");
            }
            else if (result == -1)
            {
                return String.Format("INSUFFICIENT BALANCE");
            }
            else
            {
                return String.Format("Error in the Database");
            }
           
        }

        //POST : api/Customer/deposit
        [HttpPost("deposit")]
        public decimal Deposit([FromBody] DepositModel IncomingCustomer)
        {
            CustomerModel acc = AccountHandler.GetCustomer(IncomingCustomer.AccountNumber);
            return AccountHandler.Deposit(Convert.ToDecimal(acc.Balance), IncomingCustomer.Amount, Convert.ToInt32(acc.CustomerID), Convert.ToInt32(acc.AccountID));

            
        }


        // POST : api/Customer/trasnfer
        [HttpPost("transfer")]
        public string Transfer([FromBody] TransferModel IncomingCustomer)
        {
            CustomerModel sender = AccountHandler.GetCustomer(IncomingCustomer.SenderNumber);
            CustomerModel reciever = AccountHandler.GetCustomer(IncomingCustomer.RecieverNumber);

            int result = AccountHandler.Withdraw(IncomingCustomer.Amount, sender.AccountType, Convert.ToDecimal(sender.Balance), Convert.ToInt32(sender.CustomerID), Convert.ToInt32(sender.AccountID));

            if(result > 0)
            {
                decimal newBal = AccountHandler.Deposit(Convert.ToDecimal(reciever.Balance), IncomingCustomer.Amount, Convert.ToInt32(reciever.CustomerID), Convert.ToInt32(reciever.AccountID));

                if(newBal > 0)
                {
                    return "TRANSFER SUCCESSFUL";
                }
                else
                {
                    AccountHandler.UpdateDatabase(sender.Balance, sender.AccountID);
                    return "TRANSACTION FAILED";
                }
            }
            else if(result == 0)
            {
                return "INSUFICIENT BALANCE";
            }
            else
            {
                return "TRANSFER FAILED";
            }         


        }

        // POST: api/Customer/transaction 
        [HttpPost("transaction")]
        public object Transaction([FromBody] CustomerModel IncomingDetails)
        {
            
            List<TransactionModel> listOfAll = new List<TransactionModel>();
            string qry = String.Format("SELECT TransactionID, TransactionDescription, TransactionAmount, TransactionDate FROM Histories WHERE AccountID = {0}", IncomingDetails.AccountID);
            SqlCommand cmd = new SqlCommand(qry, ConnectionHandler.ConnectObj);            
            ConnectionHandler.ConnectObj.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listOfAll.Add(item: new TransactionModel
                {
                    TransactionID = reader.GetInt32(0),
                    TransactionDescription = reader.GetString(1),
                    TransactionAmount = reader.GetDecimal(2),
                    TransactionDate = reader.GetDateTime(3)

                });
            }
                        
            
            ConnectionHandler.ConnectObj.Close();

            return listOfAll;
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
