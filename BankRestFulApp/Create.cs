using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBModels;
using System.Data.SqlClient;

namespace BankRestFulApp
{
    public class Create
    {
        public static string CreateUsers (UsersModel IncomingUser)
        {
            string uName = "Adm" + IncomingUser.AdminUsername;
            string today = DateTime.Now.ToString();

            string hashpassword = Hashpassword.ComputeSha256Hash(IncomingUser.AdminPassword);
            string qry = String.Format("insert into Admin values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", IncomingUser.AdminFirstName, IncomingUser.AdminLastName, IncomingUser.AdminEmail, uName, hashpassword, IncomingUser.AdminRole, today);
                       


            return ConnectionHandler.SqlInsert(qry);
            
        }

        public static string CreateCustomer(CustomerModel IncomingCustomer)
        {

            string qry = String.Format("insert into Customer  OUTPUT INSERTED.CustomerID values('{0}', '{1}', '{2}', '{3}')", IncomingCustomer.CustomerFirstname, IncomingCustomer.CustomerLastname, IncomingCustomer.CustomerPhone, IncomingCustomer.CustomerAddress);
            SqlCommand cmd = new SqlCommand(qry, ConnectionHandler.ConnectObj);

            ConnectionHandler.ConnectObj.Open();
            int custId = Convert.ToInt32(cmd.ExecuteScalar());
            ConnectionHandler.ConnectObj.Close();

            IncomingCustomer.AccountNumber = DateTime.UtcNow.Ticks.ToString().Substring(8);
            IncomingCustomer.Created_at = DateTime.Now.ToString();
            IncomingCustomer.PIN = new Random().Next(1000, 9999);

            string qry1 = String.Format("insert into Account  values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", IncomingCustomer.AccountNumber, IncomingCustomer.AccountType, custId, IncomingCustomer.Balance, IncomingCustomer.Created_at, IncomingCustomer.PIN);

            return ConnectionHandler.SqlInsert(qry1);

        }
    }
}
