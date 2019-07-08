using System;
namespace Project1.Models
{
    public abstract class Checking : Account
    {
        public double Credit { get; set; }
        public double Debit { get; set; }
    }
}
