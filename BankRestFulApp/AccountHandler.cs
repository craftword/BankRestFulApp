using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DBModels;


namespace BankRestFulApp
{
    public class AccountHandler
    {
        
        
        public static decimal Deposit(decimal balance, string input, int customerID, int accountID)

        {

            balance += Convert.ToDecimal(input);
            if (UpdateDatabase(balance, accountID) == 1)
            {
                string message = String.Format("Deposit {0} to the account", input);
                InsertTransaction(message, Convert.ToDecimal(input), customerID, accountID);
                return balance;
            }
            else
            {
                return 0;
            }
        }

        public static string Withdraw(string inputAmount, string accountType, decimal balance, int customerID, int accountID)
        {
            decimal input = Convert.ToDecimal(inputAmount);
            int minBalance = 0;


            if (accountType.Equals("Saving"))
            {
                minBalance = 1000;

            }


            if (input > (balance - minBalance))
            {

                return "INSUFFICIENT BALANCE";

            }
            else
            {
                balance -= input;
                if (UpdateDatabase(balance, accountID) == 1)
                {
                    string message = String.Format("Withdraw {0} from the account", input);
                    InsertTransaction(message, input, customerID, accountID);
                    return String.Format("Withdraw {0} from the account", input); 
                }
                else
                {

                    return "ERROR IN DATABASE";
                }
            }
        }

        public static decimal CheckBalance (string accountNumber)
        {
            string qry = String.Format("SELECT Balance FROM Account WHERE AccountNumber = '{0}'", accountNumber);
            SqlCommand cmd = new SqlCommand(qry, ConnectionHandler.ConnectObj);

            ConnectionHandler.ConnectObj.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                decimal bal = reader.GetDecimal(0);
                ConnectionHandler.ConnectObj.Close();
                return bal;
                
            }
            else
            {
                ConnectionHandler.ConnectObj.Close();
                return Convert.ToDecimal(0);
            }
           
        }
        private static int UpdateDatabase(decimal newBalance, int accountID)
        {
            string qry = String.Format("UPDATE Account SET Balance= {0} WHERE AccountID={1}", newBalance, accountID);
            SqlCommand cmd = new SqlCommand(qry, ConnectionHandler.ConnectObj);
            ConnectionHandler.ConnectObj.Open();
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                ConnectionHandler.ConnectObj.Close();
                return 1;

            }
            ConnectionHandler.ConnectObj.Close();
            return 0;
        }
        private static void InsertTransaction(string message, decimal input, int customerID, int accountID)
        {

            string today = DateTime.Today.ToString();
            //string qry = String.Format("insert into Transaction values('{0}', '{1}', '{2}', '{3}', '{4}')", message, input, today, customerID, accountID);
            string qry = "INSERT INTO Histories  VALUES(@message, @input, @today, @customerID, @accountID)";
            SqlCommand cmd = new SqlCommand(qry, ConnectionHandler.ConnectObj);
            ConnectionHandler.ConnectObj.Open();
            cmd.Parameters.AddWithValue("@message", message);
            cmd.Parameters.AddWithValue("@input", input);
            cmd.Parameters.AddWithValue("@today", today);
            cmd.Parameters.AddWithValue("@customerID", customerID);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            cmd.ExecuteNonQuery();
            ConnectionHandler.ConnectObj.Close();

        }

        public static  CustomerModel GetCustomer(string accountNumber)
        {
            CustomerModel listOfCustomer = new CustomerModel();
            string qry = "SELECT Customer.*, Account.* FROM Customer JOIN Account ON Customer.CustomerID = Account.CustomerID WHERE Account.AccountNumber=@usr";
            SqlCommand cmd = new SqlCommand(qry, ConnectionHandler.ConnectObj);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@usr", accountNumber);
            ConnectionHandler.ConnectObj.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    
                    listOfCustomer.CustomerID = reader.GetInt32(0);
                    listOfCustomer.CustomerFirstname = reader.GetString(1);
                    listOfCustomer.CustomerLastname = reader.GetString(2);
                    listOfCustomer.CustomerPhone = reader.GetString(3);
                    listOfCustomer.CustomerAddress = reader.GetString(4);
                    listOfCustomer.AccountID = reader.GetInt32(5);
                    listOfCustomer.AccountType = reader.GetString(7);
                    listOfCustomer.Balance = reader.GetDecimal(9);
                    listOfCustomer.PIN = reader.GetInt32(0);

                    
                }
            }
            ConnectionHandler.ConnectObj.Close();
            return listOfCustomer;
        }

    }
}

