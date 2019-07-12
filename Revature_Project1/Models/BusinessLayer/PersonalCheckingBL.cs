using System;
using System.Collections.Generic;
using System.Web;

namespace Revature_Project1.Models
{
    public class PersonalCheckingBL
    {
        public PersonalCheckingAccount Create(string accountType, string startingValue, string userID)
        {

            PersonalCheckingAccount newAccount = new PersonalCheckingAccount()
            {
                AccountID = 0,
                customerID = userID,
                Credit = int.Parse(startingValue),
                Debit = 0,
                interestRate = 2.5

            };

            return newAccount;
        }


        public String Transfer(int fromAccount, int toAccount, double amount)
        {
            /* try
             {
                 AccountBL azBL = new AccountBL();
                 var fromAcc = new AccountBL().GetAccount(fromAccount);
                 var toAcc = new AccountBL().GetAccount(toAccount);
                 //if ((toAcc is PersonalCheckingAccount) || (toAcc is BusinessCheckingAccount))
                 if ((toAcc is PersonalCheckingAccount))
                 {
                     Withdraw(fromAccount, amount);
                     new AccountBL().Deposit((IAccount)new AccountBL().getType(toAcc), toAccount, amount);
                     return ($"Your transfer of ${amount} was completed from account {fromAccount} to account {toAccount}");
                 }
                 throw new Exception();

             }
             catch
             {
                 throw;
             }*/
            return null;
        }
    }
}
