using System;
using System.Collections.Generic;
using System.Text;

namespace DBModels
{
    class CustomerModel
    {
        
        public string CustomerFirstname { get; set; }
        public string CustomerLastname { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public double Balance { get; set; }
        public string Created_at { get; set; }
        public string PIN { get; set; }
    }
}
