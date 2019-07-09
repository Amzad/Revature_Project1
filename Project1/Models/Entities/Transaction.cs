using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project1.Models
{
    public class Transaction
    {
        [Key]
        public string id { get; set; }
        public string accountID { get; set; }
        public string transactionMessage { get; set; }
    }
}