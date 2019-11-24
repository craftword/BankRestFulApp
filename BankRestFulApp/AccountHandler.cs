using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace BankRestFulApp
{
    public class AccountHandler
    {
        public static double Deposit(double balance, string input, int customerID, int accountID)

        {

            balance += Convert.ToDouble(input);
            if (UpdateDatabase(balance, accountID) == 1)
            {
                string message = String.Format("Deposit {0} to the account", input);
                InsertTransaction(message, Convert.ToDouble(input), customerID, accountID);
                return balance;
            }
            else
            {
                return 0;
            }
        }

        public static double Withdraw(string inputAmount, string accountType, double balance, int customerID, int accountID)
        {
            double input = Convert.ToDouble(inputAmount);
            int minBalance = 0;


            if (accountType.Equals("Saving"))
            {
                minBalance = 1000;

            }


            if (input > (balance - minBalance))
            {

                return 0;

            }
            else
            {
                balance -= input;
                if (UpdateDatabase(balance, accountID) == 1)
                {
                    string message = String.Format("Withdraw {0} from the account", input);
                    InsertTransaction(message, input, customerID, accountID);
                    return balance;
                }
                else
                {

                    return -1;
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
        private static int UpdateDatabase(double newBalance, int accountID)
        {
            string qry = String.Format("UPDATE Account SET Balance= {0} WHERE AccountID={1}", newBalance, accountID);
            SqlCommand cmd = new SqlCommand(qry, ConnectionHandler.ConnectObj);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                return 1;

            }
            return 0;
        }
        private static void InsertTransaction(string message, double input, int customerID, int accountID)
        {

            string today = DateTime.Today.ToString();
            //string qry = String.Format("insert into Transaction values('{0}', '{1}', '{2}', '{3}', '{4}')", message, input, today, customerID, accountID);
            string qry = "INSERT INTO Histories  VALUES(@message, @input, @today, @customerID, @accountID)";
            SqlCommand cmd = new SqlCommand(qry, ConnectionHandler.ConnectObj);
            cmd.Parameters.AddWithValue("@message", message);
            cmd.Parameters.AddWithValue("@input", input);
            cmd.Parameters.AddWithValue("@today", today);
            cmd.Parameters.AddWithValue("@customerID", customerID);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            cmd.ExecuteNonQuery();


        }
    }
}

