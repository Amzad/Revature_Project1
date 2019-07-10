using System;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public abstract class Checking : Account
    {
        [Display(Name = "Balance")]
        public double Credit { get; set; }
        public double Debit { get; set; }
    }
}
