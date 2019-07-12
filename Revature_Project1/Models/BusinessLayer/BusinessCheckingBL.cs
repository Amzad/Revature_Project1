using System;
using System.Collections.Generic;
using System.Web;

namespace Revature_Project1.Models
{
    public class BusinessCheckingBL
    {
        public BusinessCheckingAccount Create(string accountType, string startingValue, string userID)
        {
            BusinessCheckingAccount newAccount = new BusinessCheckingAccount()
            {
                AccountID = 0,
                customerID = userID,
                Credit = int.Parse(startingValue),
                Debit = 0,
                interestRate = 3.5

            };

            return newAccount;
        }
        

        public void Deposit(string accountID, string Credit, string Debit, string depositvalue, string userID)
        {
            /*try
            {
                double doubleCredit = double.Parse(Credit);
                double doubleDeposit = double.Parse(depositvalue);
                double doubleDebit = double.Parse(Debit);
                //double setAmount = doubleCredit - doubleDeposit;

                if (doubleCredit > 0) 
                {
                    double setAmount = doubleCredit + doubleDeposit;
                    ba.Debit = 0;
                    ba.Credit = setAmount;
                    Transaction ta = new Transaction()
                    {
                        id = 0,
                        accountID = int.Parse(accountID),
                        transactionMessage = "Deposit of " + depositvalue
                    };
                    _db.Transactions.Add(ta);
                    _db.Entry(ba).State = EntityState.Modified;
                    _db.SaveChanges();
                    return ba;
                    return new BusinessCheckingDAL().Deposit(int.Parse(accountID), doubleDeposit, setAmount, userID);
                }
                else if (doubleDebit > 0)
                {
                    double remainder = -doubleDebit + doubleDeposit; // 200(balance = -300(debit + 500(deposit
                    if (remainder <= 0)
                    { 
                        // when balance stays in debit after deposit
                        ba.Credit = 0;
                        ba.Debit = -remainder;
                        Transaction ta = new Transaction()
                        {
                            id = 0,
                            accountID = int.Parse(accountID),
                            transactionMessage = "Deposit of " + depositvalue
                        };
                        _db.Transactions.Add(ta);
                        _db.Entry(ba).State = EntityState.Modified;
                        _db.SaveChanges();
                        return ba;
                    }
                    else
                    {
                        // when balance goes from debit to credit
                        ba.Credit = remainder;
                        ba.Debit = 0;
                        Transaction ta = new Transaction()
                        {
                            id = 0,
                            accountID = int.Parse(accountID),
                            transactionMessage = "Deposit of " + depositvalue
                        };
                        _db.Transactions.Add(ta);
                        _db.Entry(ba).State = EntityState.Modified;
                        _db.SaveChanges();
                        return ba;
                    }
                }
                else
                {
                    // Depositing when credit and debit = 0;
                    //return new BusinessCheckingDAL().Deposit(int.Parse(accountID), doubleDeposit, doubleDeposit, userID);
                    ba.Credit = doubleDeposit;
                    ba.Debit = 0;
                    Transaction ta = new Transaction()
                    {
                        id = 0,
                        accountID = int.Parse(accountID),
                        transactionMessage = "Deposit of " + depositvalue
                    };
                    _db.Transactions.Add(ta);
                    _db.Entry(ba).State = EntityState.Modified;
                    _db.SaveChanges();
                    return ba;
                }*/
            
        }

        public void Transfer(int fromAccount, int toAccount, double amount)
        {
            /*try
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
            }*/
            //return null;
        }
    }
}
