using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBModels;

namespace BankRestFulApp
{
    public static class Login
    {
        public static string AdminLogin (LoginModel loginDetails)
        {
            string hashpassword = Hashpassword.ComputeSha256Hash(loginDetails.AdminPassword);

            string qry = String.Format("SELECT 1 FROM Admin where AdminUsername='{0}' and AdminPassword='{1}'", loginDetails.AdminUsername, hashpassword);
            
            return ConnectionHandler.SqlQuery(qry);
        }
        public static string CustomerLogin(LoginModel loginDetails)
        {
            string qry = String.Format("SELECT Customer.CustomerFirstname, Customer.CustomerLastname, Account.* FROM Customer JOIN Account ON Customer.CustomerID = Account.CustomerID WHERE Account.AccountNumber={0} and Account.PIN={1} ", loginDetails.AdminUsername, loginDetails.AdminPassword);

            return ConnectionHandler.SqlQuery(qry);
        }
    }
}
