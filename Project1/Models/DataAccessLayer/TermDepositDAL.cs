using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1.Models
{
    public class TermDepositDAL
    {
        public void Create(TermDepositAccount cust)
        {
            var context = new ApplicationDbContext();
            context.TermDepositAccounts.Add(cust);
            context.SaveChanges();
        }

        public List<TermDepositAccount> GetList()
        {
            var context = new ApplicationDbContext();
            return context.TermDepositAccounts.ToList();
        }

        public String Withdraw(TermDepositAccount ta, double amount)
        {
            ta.Credit = amount;
            ta.transactionLog.Add("Withdrawal of " + amount);
            return ($"Your new balance for account {ta.AccountID} is ${ta.Credit}");

        }

    }
}

