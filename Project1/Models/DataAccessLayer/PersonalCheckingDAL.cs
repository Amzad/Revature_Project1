using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace Project1.Models
{
    public class PersonalCheckingDAL : AccountDAL
    {
        public void Create(PersonalCheckingAccount cust)
        {
                var context = new ApplicationDbContext();
                context.CheckingAccounts.Add(cust);
                context.SaveChanges();
        }

        public List<PersonalCheckingAccount> GetList()
        {
            var context = new ApplicationDbContext();
            return context.CheckingAccounts.ToList();
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
                //personalCheckingAccount.transactionLog.Add("Withdrawal of " + withdrawvalue);
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

        public String Deposit(PersonalCheckingAccount pa, double amount, double balance)
        {
            pa.Credit = balance;
            pa.transactionLog.Add("Deposit of " + amount);
            return ($"Your new balance for account {pa.AccountID} is ${pa.Credit}");
        }
    }
}

