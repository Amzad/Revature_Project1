using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1.Models
{
    public class LoanDAL : AccountDAL
    { 
        public void Create(LoanAccount cust)
        {
            var context = new ApplicationDbContext();
            context.LoanAccounts.Add(cust);
            context.SaveChanges();
        }

        public List<LoanAccount> GetList()
        {
            var context = new ApplicationDbContext();
            return context.LoanAccounts.ToList();
        }

        public String PayInstallment(LoanAccount account, double amount)
        {
            account.Debit = amount;
            account.transactionLog.Add("Payment of " + amount);
            return $"A payment of {amount} has been made. Your new loan balance is {account.Debit}";
        }
    }
}
