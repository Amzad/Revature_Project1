using System;
using System.Collections.Generic;
using System.Web;

namespace Revature_Project1.Models
{
    public class LoanBL
    {
        public LoanAccount Create(string loanamount, string userID)
        {
            LoanAccount newAccount = new LoanAccount()
            {
                customerID = userID,
                Debit = int.Parse(loanamount),
                interestRate = 6.5

            };
            return newAccount;
        }

        public string Deposit(int accountID, double amount)
        {
            throw new Exception();
        }

       /* public String PayInstallment(int accountID, double amount)
        {
            LoanAccount la = AccountDAL.accountList.Find(acc => acc.AccountID == accountID) as LoanAccount;
            return new LoanDAL().PayInstallment(la, amount);

        }*/

    }
}
