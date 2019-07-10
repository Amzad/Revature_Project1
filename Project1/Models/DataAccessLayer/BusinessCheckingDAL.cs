using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project1.Models
{
    public class BusinessCheckingDAL : AccountDAL
    {
        ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public void Create(BusinessCheckingAccount cust)
        {
            var context = new ApplicationDbContext();
            context.BusinessAccounts.Add(cust);
            context.SaveChanges();
        }

        public List<BusinessCheckingAccount> GetList()
        {
            var context = new ApplicationDbContext();
            return context.BusinessAccounts.Where(c => c.customerID == user.Id).ToList();
        }

        public BusinessCheckingAccount Withdraw(int naccountID, double Credit, double withdrawvalue)
        {
            var db = new ApplicationDbContext();
            BusinessCheckingAccount ba = db.BusinessAccounts.Find(naccountID);


            if (Credit >= 0) // If credit is still posible after withdrawl
            {
                ba.Debit = 0;
                ba.Credit = Credit;
                Transaction ta = new Transaction()
                {
                    id = 0,
                    accountID = naccountID,
                    transactionMessage = "Withdrawal of " + withdrawvalue
                };
                db.Transactions.Add(ta);
                db.Entry(ba).State = EntityState.Modified;
                db.SaveChanges();
                return ba;
            }
            else
            {
                ba.Credit = 0;
                ba.Debit = Math.Abs(Credit);

                Transaction ta = new Transaction()
                {
                    id = 0,
                    accountID = naccountID,
                    transactionMessage = "Withdrawal of " + withdrawvalue
                };
                return ba;
            }


        }

        public BusinessCheckingAccount Deposit(int naccountID, double Credit, double depositvaluee)
        {
            var db = new ApplicationDbContext();
            BusinessCheckingAccount ba = db.BusinessAccounts.Find(naccountID);
            try
            { 
                if (Credit >= 0) 
                {
                    ba.Debit = 0;
                    ba.Credit = Credit;
                    Transaction ta = new Transaction()
                    {
                        id = 0,
                        accountID = naccountID,
                        transactionMessage = "Deposit of " + depositvaluee
                    };
                    db.Transactions.Add(ta);
                    db.Entry(ba).State = EntityState.Modified;
                    db.SaveChanges();
                    return ba;
                }
                else
                {
                    ba.Credit = 0;
                    ba.Debit = -Credit;
                    Transaction ta = new Transaction()
                    {
                        id = 0,
                        accountID = naccountID,
                        transactionMessage = "Deposit of " + depositvaluee
                    };
                    db.Transactions.Add(ta);
                    db.Entry(ba).State = EntityState.Modified;
                    db.SaveChanges();
                    return ba;
                }
            }
            catch
            {
                throw;
            }
        }

        public Account getAccount(int accountID)
        {
            throw new NotImplementedException();
        }

        public List<Account> getAllAccounts(int accountID)
        {
            throw new NotImplementedException();
        }
    }
}
