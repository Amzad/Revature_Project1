using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class Customer
    {
        public int ID { get; set; }

        //[Required]
        public string FirstName { get; set; }
        //[Required]
        public string LastName { get; set; }

        //[EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public virtual List<int> Accounts { get; set; }

    }
}
