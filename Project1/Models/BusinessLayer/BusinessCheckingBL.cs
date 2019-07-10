using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Web;

namespace Project1.Models
{
    public class BusinessCheckingBL : IAccount
    {
        public void Create(string accountType, string startingValue)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            BusinessCheckingDAL accountDAL = new BusinessCheckingDAL();
            //int accountIDCount = accountDAL.GetCount();

            BusinessCheckingAccount newAccount = new BusinessCheckingAccount()
            {
                AccountID = 0,
                customerID = user.Id,
                Credit = int.Parse(startingValue),
                Debit = 0,
                interestRate = 3.5

            };

            accountDAL.Create(newAccount);
        }

        public List<BusinessCheckingAccount> GetList()
        {
            return new BusinessCheckingDAL().GetList();

        }

        public String Withdraw(int accountID, double amount)
        {
            try
            {

               /* BusinessCheckingAccount ba = AccountDAL.accountList.Find(account => account.AccountID == accountID) as BusinessCheckingAccount;
                if (ba.Credit > 0)
                {
                    double setAmount = ba.Credit - amount;
                    if (setAmount < 0)
                    {
                        return new BusinessCheckingDAL().Withdraw(ba, amount, setAmount); // return negative value for debit
                    } else
                    {
                        return new BusinessCheckingDAL().Withdraw(ba, amount, setAmount); // return positive value for credit
                    }
                }
                else if (ba.Debit > 0)
                {
                    double remainder = ba.Debit + amount;
                    return new BusinessCheckingDAL().Withdraw(ba, amount, -remainder);
                }
                else
                {
                    return new BusinessCheckingDAL().Withdraw(ba, amount, -amount); 
                }*/
            }
            catch
            {
                throw;
            }
            return null;
        }

        public String Deposit(int accountID, double amount)
        {
            try
            {
                /*BusinessCheckingAccount ba = AccountDAL.accountList.Find(account => account.AccountID == accountID) as BusinessCheckingAccount;
                if (ba.Credit > 0)
                {
                    double setAmount = ba.Credit + amount;
                    return new BusinessCheckingDAL().Deposit(ba, amount, setAmount);
                }
                else if (ba.Debit > 0)
                {  
                    double remainder = ba.Debit - amount; // -4700 = 300 - 5000
                    if (remainder <= 0)
                    {

                        return new BusinessCheckingDAL().Deposit(ba, amount,  Math.Abs(remainder));
                    }
                    else
                    {
                        return new BusinessCheckingDAL().Deposit(ba, amount, -remainder);
                    }
                }
                else
                {
                    return new BusinessCheckingDAL().Deposit(ba, amount, amount);
                }*/
            }
            catch
            {
                throw;
            }
            return null;

        }

        public String Transfer(int fromAccount, int toAccount, double amount)
        {
            try
            {
                AccountBL azBL = new AccountBL();
                var fromAcc = new AccountBL().GetAccount(fromAccount);
                var toAcc = new AccountBL().GetAccount(toAccount);
                if ((toAcc is PersonalCheckingAccount) || (toAcc is BusinessCheckingAccount))
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
            }
        }
    }
}
