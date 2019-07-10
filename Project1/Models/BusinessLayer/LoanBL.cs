using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Web;

namespace Project1.Models
{
    public class LoanBL : IAccount
    {
        public void Create(string loanamount)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            LoanDAL accountDAL = new LoanDAL();

            LoanAccount newAccount = new LoanAccount()
            {
                customerID = user.Id,
                Debit = int.Parse(loanamount),
                interestRate = 6.5

            };
            accountDAL.Create(newAccount);
        }

        public List<LoanAccount> GetList()
        {
            return new LoanDAL().GetList();

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

        public string Withdraw(int accountID, double amount)
        {
            throw new Exception();

        }

        public String Transfer(int fromAccount, int toAccount, double amount)
        {

            throw new Exception();
        }
    }
}
