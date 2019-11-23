using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBModels;

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
            
            string qry = String.Format("insert into Admin values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", IncomingCustomer.AdminFirstName, IncomingCustomer.AdminLastName, IncomingCustomer.AdminEmail, uName, hashpassword, IncomingCustomer.AdminRole, today);

            return ConnectionHandler.SqlInsert(qry);

        }
    }
}
