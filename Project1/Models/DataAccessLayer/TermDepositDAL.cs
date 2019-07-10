using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1.Models
{
    public class TermDepositDAL
    {
        ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        //Test
        public void Create(TermDepositAccount cust)
        {
            var context = new ApplicationDbContext();
            context.TermDepositAccounts.Add(cust);
            context.SaveChanges();
        }

        public List<TermDepositAccount> GetList()
        {
            var context = new ApplicationDbContext();
            return context.TermDepositAccounts.Where(c => c.customerID == user.Id).ToList();
        }

        public String Withdraw(TermDepositAccount ta, double amount)
        {
            ta.Credit = amount;
            ta.transactionLog.Add("Withdrawal of " + amount);
            return ($"Your new balance for account {ta.AccountID} is ${ta.Credit}");

        }

    }
}

