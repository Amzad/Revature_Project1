﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1.Models
{
    public class BusinessCheckingDAL : AccountDAL
    {
        public void Create(BusinessCheckingAccount cust)
        {
            var context = new ApplicationDbContext();
            context.BusinessAccounts.Add(cust);
            context.SaveChanges();
        }

        public List<BusinessCheckingAccount> GetList()
        {
            var context = new ApplicationDbContext();
            return context.BusinessAccounts.ToList();
        }

        public String Withdraw(BusinessCheckingAccount ba, double amount, double balance)
        {

            if (balance >= 0) // If credit is still posible after withdrawl
            {
                ba.Debit = 0;
                ba.Credit = balance;
                ba.transactionLog.Add("Withdrawal of " + amount);
                return ($"Your new balance for account {ba.AccountID} is ${ba.Credit}");
            }
            else
            {
                ba.Credit = 0;
                ba.Debit = Math.Abs(balance);
                ba.transactionLog.Add("Withdrawal of " + amount);
                return ($"Your new balance for account {ba.AccountID} is -${ba.Debit}");
            }


        }

        public String Deposit(BusinessCheckingAccount ba, double amount, double balance)
        {
            try
            {
                if (balance >= 0) 
                {
                    ba.Debit = 0;
                    ba.Credit = balance;
                    ba.transactionLog.Add("Deposit of " + amount);
                    return ($"Your new balance for account {ba.AccountID} is ${ba.Credit}");
                }
                else
                {
                    ba.Credit = 0;
                    ba.Debit = -balance;
                    ba.transactionLog.Add("Deposit of " + amount);
                    return ($"Your new balance for account {ba.AccountID} is -${ba.Debit}");
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
