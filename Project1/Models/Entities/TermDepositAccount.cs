﻿using System;
namespace Project1.Models
{
    public class TermDepositAccount : Account
    {
        public double Credit { get; set; }
        public int depositTerm { get; set; }
    }
}
