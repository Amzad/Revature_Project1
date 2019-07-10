using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Web;

namespace Project1.Models
{
    public class TermDepositBL : IAccount
    {

        public void Create(string loanamount, string loanlength)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            TermDepositDAL accountDAL = new TermDepositDAL();

            TermDepositAccount newAccount = new TermDepositAccount()
            {
                customerID = user.Id,
                Credit = int.Parse(loanamount),
                interestRate = 1.5,
                depositTerm = int.Parse(loanlength)

            };
            accountDAL.Create(newAccount);
        }

        public List<TermDepositAccount> GetList()
        {
            return new TermDepositDAL().GetList();

        }

        // Not used. Throw unimplementederror.
        public string Deposit(int accountID, double amount)
        {
            throw new Exception();
        }

        public String Withdraw(int accountID, double amount)
        {
            try
            {
                /*TermDepositAccount ta = AccountDAL.accountList.Find(account => account.AccountID == accountID) as TermDepositAccount;

                if (ta.depositTerm == 0)
                {
                    ta.Credit -= amount;
                    ta.transactionLog.Add("Withdrawal of " + amount);
                    return new TermDepositDAL().Withdraw(ta, amount);
                }
                else
                {
                    throw new Exception();
                }*/
            }
            catch (Exception)
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
