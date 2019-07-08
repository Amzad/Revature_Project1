using System;
using System.Collections.Generic;

namespace Project1.Models
{ 
    public abstract class Account
    {
        public int AccountID { get; set; }
        public int customerID { get; set; }
        public double interestRate { get; set; }
        public ICollection<String> transactionLog { get; set; }

    }
}
