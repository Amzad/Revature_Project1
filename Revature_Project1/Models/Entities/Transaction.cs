using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Revature_Project1.Models
{
    public class Transaction
    {
        public int id { get; set; }
        public int accountID { get; set; }
        public string accountType { get; set; }
        public string transactionMessage { get; set; }
    }
}