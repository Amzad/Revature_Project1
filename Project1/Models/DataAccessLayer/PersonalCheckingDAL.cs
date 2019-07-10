using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project1.Models
{
    public class PersonalCheckingDAL : AccountDAL
    {
        ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        public void Create(PersonalCheckingAccount cust)
        {
                var context = new ApplicationDbContext();
                context.CheckingAccounts.Add(cust);
                context.SaveChanges();
        }

        public List<PersonalCheckingAccount> GetList()
        {
            var context = new ApplicationDbContext();
            return context.CheckingAccounts.Where(c => c.customerID == user.Id).ToList(); 
        }


        public int GetCount()
        {
            int customers = new ApplicationDbContext().CheckingAccounts.Count() + 1000;
            return customers;
        } 

        public PersonalCheckingAccount Withdraw(int naccountID, double Credit, double withdrawvalue)
        {
            try
            {
                var db = new ApplicationDbContext();
                PersonalCheckingAccount personalCheckingAccount = db.CheckingAccounts.Find(naccountID);
                personalCheckingAccount.Credit = Credit;
                Transaction ta = new Transaction()
                {
                    id = 0,
                    accountID = naccountID,
                    transactionMessage = "Withdrawal of " + withdrawvalue
                };
                db.Transactions.Add(ta);
                db.Entry(personalCheckingAccount).State = EntityState.Modified;
                db.SaveChanges();
                return personalCheckingAccount;
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }

        public PersonalCheckingAccount Deposit(int naccountID, double Credit, double depositvalue)
        {
            try
            {
                var db = new ApplicationDbContext();
                PersonalCheckingAccount personalCheckingAccount = db.CheckingAccounts.Find(naccountID);
                personalCheckingAccount.Credit = Credit;
                Transaction ta = new Transaction()
                {
                    id = 0,
                    accountID = naccountID,
                    transactionMessage = "Deposit of " + depositvalue
                };
                db.Transactions.Add(ta);
                db.Entry(personalCheckingAccount).State = EntityState.Modified;
                db.SaveChanges();
                return personalCheckingAccount;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }
    }
}

