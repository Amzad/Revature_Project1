using System;
using System.Collections.Generic;
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

        public String Withdraw(PersonalCheckingAccount pa, double amount, double balance)
        {
            pa.Credit = balance;
            pa.transactionLog.Add("Withdrawal of " + amount);
            return ($"Your new balance for account {pa.AccountID} is ${pa.Credit}");

        }

        public String Deposit(PersonalCheckingAccount pa, double amount, double balance)
        {
            pa.Credit = balance;
            pa.transactionLog.Add("Deposit of " + amount);
            return ($"Your new balance for account {pa.AccountID} is ${pa.Credit}");
        }
    }
}

