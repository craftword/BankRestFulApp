using System;
using System.Collections.Generic;
using System.Text;

namespace DBModels
{
    public class CustomerModel
    {

        public int CustomerID { get; set; }
        public string CustomerFirstname { get; set; }
        public string CustomerLastname { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public int AccountID { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public string Created_at { get; set; }
        public int PIN { get; set; }

        

    }
}
