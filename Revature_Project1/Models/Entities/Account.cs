﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Revature_Project1.Models
{
    public abstract class Account
    {
        [Key]
        public int AccountID { get; set; }
        public string customerID { get; set; }
        public double interestRate { get; set; }

    }
}