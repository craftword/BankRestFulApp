using System;
using System.Collections.Generic;
using System.Text;

namespace DBModels
{
    public class TransactionModel
    {
        public int TransactionID { get; set; }
        public string TransactionDescription { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        
    }
}
