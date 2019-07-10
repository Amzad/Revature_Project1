using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Web;

namespace Project1.Models
{
    public class PersonalCheckingBL : IAccount
    {
        public void Create(string accountType, string startingValue)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            PersonalCheckingDAL accountDAL = new PersonalCheckingDAL();
            int accountIDCount = accountDAL.GetCount();

            PersonalCheckingAccount newAccount = new PersonalCheckingAccount()
            {
                AccountID = accountIDCount,
                customerID = user.Id,
                Credit = int.Parse(startingValue),
                Debit = 0,
                interestRate = 2.5

            };

            accountDAL.Create(newAccount);
        }

        public List<PersonalCheckingAccount> GetList()
        {
            return new PersonalCheckingDAL().GetList();

        }

        public String Withdraw(int accountID, double amount)
        {
            try
            {
                /*PersonalCheckingAccount pa = AccountDAL.accountList.Find(account => account.AccountID == accountID) as PersonalCheckingAccount;
                double balance = pa.Credit - amount;
                if (balance >= 0)
                {
                    return new PersonalCheckingDAL().Withdraw(pa, amount, balance);
                }
                else
                {
                    throw new Exception();
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
                /*PersonalCheckingAccount pa = AccountDAL.accountList.Find(account => account.AccountID == accountID) as PersonalCheckingAccount;
                double balance = pa.Credit + amount;
                return new PersonalCheckingDAL().Deposit(pa, amount, balance);*/

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
            }
        }
    }
}
