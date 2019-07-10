using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Web;

namespace Project1.Models
{
    public class BusinessCheckingBL
    {
        public void Create(string accountType, string startingValue)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            BusinessCheckingDAL accountDAL = new BusinessCheckingDAL();
            //int accountIDCount = accountDAL.GetCount();

            BusinessCheckingAccount newAccount = new BusinessCheckingAccount()
            {
                AccountID = 0,
                customerID = user.Id,
                Credit = int.Parse(startingValue),
                Debit = 0,
                interestRate = 3.5

            };

            accountDAL.Create(newAccount);
        }

        public List<BusinessCheckingAccount> GetList()
        {
            return new BusinessCheckingDAL().GetList();

        }

        public BusinessCheckingAccount Withdraw(string accountID, string Credit, string Debit, string withdrawvalue)
        {
            try
            {
                double doubleCredit = double.Parse(Credit);
                double doubleWithdraw = double.Parse(withdrawvalue);
                double doubleDebit = double.Parse(Debit);
                double setAmount = doubleCredit - doubleWithdraw;

                if (doubleCredit > 0)
                {
                    if (setAmount < 0)
                    {
                        return new BusinessCheckingDAL().Withdraw(int.Parse(accountID), setAmount, doubleWithdraw); // return negative value for debit
                    }
                }
                else if (doubleDebit > 0)
                {
                    double remainder = doubleDebit + setAmount;
                    return new BusinessCheckingDAL().Withdraw(int.Parse(accountID), setAmount, -remainder);
                }
                else
                {
                    return new BusinessCheckingDAL().Withdraw(int.Parse(accountID), setAmount, -doubleWithdraw);
                }
            }
            catch
            {
                throw;
            }
            return null;
        }

        public BusinessCheckingAccount Deposit(string accountID, string Credit, string Debit, string depositvalue)
        {
            try
            {
                double doubleCredit = double.Parse(Credit);
                double doubleDeposit = double.Parse(depositvalue);
                double doubleDebit = double.Parse(Debit);
                //double setAmount = doubleCredit - doubleDeposit;

                if (doubleCredit > 0)
                {
                    double setAmount = doubleCredit + doubleDeposit;
                    return new BusinessCheckingDAL().Deposit(int.Parse(accountID), doubleDeposit, setAmount);
                }
                else if (doubleDebit > 0)
                {
                    double remainder = doubleDebit - doubleDeposit; // -4700 = 300 - 5000
                    if (remainder <= 0)
                    {

                        return new BusinessCheckingDAL().Deposit(int.Parse(accountID), doubleDeposit, Math.Abs(remainder));
                    }
                    else
                    {
                        return new BusinessCheckingDAL().Deposit(int.Parse(accountID), doubleDeposit, -remainder);
                    }
                }
                else
                {
                    return new BusinessCheckingDAL().Deposit(int.Parse(accountID), doubleDeposit, doubleDeposit);
                }
            }
            catch
            {
                throw;

            }
        }

        public String Transfer(int fromAccount, int toAccount, double amount)
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
            return null;
        }
    }
}
