﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BankRestFulApp
{
    public class ConnectionHandler
    {
        static SqlConnection con = null;
        public static SqlConnection ConnectObj
        {
            get
            {
                if (con == null)
                    con = new SqlConnection("Data Source=DESKTOP-1CH1I23;Initial Catalog=BankApp;Integrated Security=True");
                return con;
            }
        }

        public static string SqlQuery (string qry)
        {
            SqlCommand cmd = new SqlCommand(qry, ConnectObj);
            ConnectObj.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                ConnectObj.Close();
                return "LOGIN SUCCESSFULLY";
            }
            else
            {
                ConnectObj.Close();
                return "INVALID USERNAME OR PASSWORD";
            }
            

        }
        public static string SqlInsert(string qry)
        {
            SqlCommand cmd = new SqlCommand(qry, ConnectObj);
            ConnectObj.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i >= 0)
                {
                    ConnectObj.Close();
                    return "Insert Action was sucessfull";

                }
            }
            catch (Exception ex)
            {
                return "The following error occurred during the write operation:" + ex.Message;
            }

            return "0";
        }
    }
}
