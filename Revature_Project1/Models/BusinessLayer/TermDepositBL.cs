using System;
using System.Collections.Generic;
using System.Web;

namespace Revature_Project1.Models
{
    public class TermDepositBL
    {

        public TermDepositAccount Create(string loanamount, string loanlength, string userID)
        {
            TermDepositAccount newAccount = new TermDepositAccount()
            {
                customerID = userID,
                Credit = int.Parse(loanamount),
                interestRate = 1.5,
                depositTerm = int.Parse(loanlength)

            };
            return newAccount;
        }

    }
}
