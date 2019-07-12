using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Revature_Project1.Models;
using Revature_Project1.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Revature_Project1Controllers
{
    public class BusinessCheckingController : Controller
    {
        MSSQLContext _db;

        public BusinessCheckingController(MSSQLContext db)
        {
            _db = db;
        }

        public ActionResult Withdraw(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            BusinessCheckingAccount businessCheckingAccount = _db.BusinessAccounts.Find(id);
            if (businessCheckingAccount == null)
            {
                return NotFound();
            }
            return View(businessCheckingAccount);
        }

        public ActionResult TransferFrom(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            BusinessCheckingAccount personalCheckingAccount = _db.BusinessAccounts.Find(id);

            if (personalCheckingAccount == null)
            {
                return NotFound();
            }
            return View(personalCheckingAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TransferFrom(string AccountID, string Credit, string transfervalue)
        {
            ViewBag.AccountIDFrom = AccountID;
            ViewBag.CreditFrom = Credit;
            ViewBag.TransferValue = transfervalue;

            return View("TransferTo");
        }

        public ActionResult TransferTo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            BusinessCheckingAccount personalCheckingAccount = _db.BusinessAccounts.Find(id);

            if (personalCheckingAccount == null)
            {
                return NotFound();
            }
            return View(personalCheckingAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Withdraw(string accountID, string Credit, string Debit, string withdrawvalue)
        {
            try
            {
                //var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                int accID = int.Parse(accountID);


                double doubleCredit = double.Parse(Credit);
                double doubleWithdraw = double.Parse(withdrawvalue);
                double doubleDebit = double.Parse(Debit);
                double setAmount = doubleCredit - doubleWithdraw;

                BusinessCheckingAccount ba = _db.BusinessAccounts.Find(accID) as BusinessCheckingAccount;
                if (doubleCredit > 0 && setAmount <= 0)
                {
                    ba.Credit = 0;
                    ba.Debit = Math.Abs(setAmount);

                    Transaction ta = new Transaction()
                    {
                        id = 0,
                        accountID = int.Parse(accountID),
                        transactionMessage = "Withdrawal of " + withdrawvalue
                    };
                    _db.Transactions.Add(ta);
                    _db.Entry(ba).State = EntityState.Modified;
                    _db.SaveChanges();
                    //return ba;
                }
                else if (doubleCredit > 0 && setAmount > 0)
                {
                    ba.Credit = setAmount;
                    ba.Debit = 0;

                    Transaction ta = new Transaction()
                    {
                        id = 0,
                        accountID = int.Parse(accountID),
                        transactionMessage = "Withdrawal of " + withdrawvalue
                    };
                    _db.Transactions.Add(ta);
                    _db.Entry(ba).State = EntityState.Modified;
                    _db.SaveChanges();
                    //return ba;
                }
                else if (doubleDebit > 0)
                {
                    double remainder = doubleDebit + doubleWithdraw;
                    ba.Credit = 0;
                    ba.Debit = Math.Abs(remainder);

                    Transaction ta = new Transaction()
                    {
                        id = 0,
                        accountID = int.Parse(accountID),
                        transactionMessage = "Withdrawal of " + withdrawvalue
                    };
                    _db.Transactions.Add(ta);
                    _db.SaveChanges();
                    //return ba;

                }
                else
                {
                    ba.Debit = doubleWithdraw;
                    ba.Credit = 0;
                    Transaction ta = new Transaction()
                    {
                        id = 0,
                        accountID = int.Parse(accountID),
                        transactionMessage = "Withdrawal of " + withdrawvalue
                    };
                    _db.Transactions.Add(ta);
                    _db.Entry(ba).State = EntityState.Modified;
                    _db.SaveChanges();
                    //return ba;

                }
                ViewBag.Confirm = $"Your withdrawal of {withdrawvalue} was completed from account {accountID}";
                return View("Confirmed");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Deposit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            BusinessCheckingAccount businessCheckingAccount = _db.BusinessAccounts.Find(id);
            if (businessCheckingAccount == null)
            {
                return NotFound();
            }
            return View(businessCheckingAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit(string accountID, string Credit, string Debit, string depositvalue)
        {
            try
            {
                double doubleCredit = double.Parse(Credit);
                double doubleDeposit = double.Parse(depositvalue);
                double doubleDebit = double.Parse(Debit);
                //double setAmount = doubleCredit - doubleDeposit;
                int accID = int.Parse(accountID);
                BusinessCheckingAccount ba = _db.BusinessAccounts.Find(accID) as BusinessCheckingAccount;

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
                    //return ba;
                    //return new BusinessCheckingDAL().Deposit(int.Parse(accountID), doubleDeposit, setAmount, userID);
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
                        //return ba;
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
                        //return ba;
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
                    //return ba;
                }

                ViewBag.Confirm = $"Your deposit of {depositvalue} was completed from account {accountID}";
                return View("Confirmed");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Transfer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            BusinessCheckingAccount personalCheckingAccount = _db.BusinessAccounts.Find(id);

            if (personalCheckingAccount == null)
            {
                return NotFound();
            }
            return View(personalCheckingAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer(string AccountID, string CreditFrom, string TransferValue, string FromAccID, string accountToType)
        {
            try
            {
                //var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                int accID = int.Parse(FromAccID);
                BusinessCheckingAccount ba = _db.BusinessAccounts.Find(accID) as BusinessCheckingAccount;

                double doubleCredit = ba.Credit;
                double doubleWithdraw = double.Parse(TransferValue);
                double doubleDebit = ba.Debit;
                double setAmount = doubleCredit - doubleWithdraw;

                if (doubleCredit > 0 && setAmount <= 0)
                {
                    ba.Credit = 0;
                    ba.Debit = Math.Abs(setAmount);

                    Transaction ta = new Transaction()
                    {
                        id = 0,
                        accountID = int.Parse(FromAccID),
                        transactionMessage = "Withdrawal of " + TransferValue
                    };
                    _db.Transactions.Add(ta);
                    _db.Entry(ba).State = EntityState.Modified;
                    //_db.SaveChanges();
                    //return ba;
                }
                else if (doubleDebit > 0)
                {
                    double remainder = doubleDebit + doubleWithdraw;
                    ba.Credit = 0;
                    ba.Debit = Math.Abs(remainder);

                    Transaction ta = new Transaction()
                    {
                        id = 0,
                        accountID = int.Parse(FromAccID),
                        transactionMessage = "Withdrawal of " + TransferValue
                    };
                    _db.Transactions.Add(ta);
                    //_db.SaveChanges();
                    //return ba;

                }
                else
                {
                    ba.Debit = doubleWithdraw;
                    ba.Credit = 0;
                    Transaction ta = new Transaction()
                    {
                        id = 0,
                        accountID = int.Parse(FromAccID),
                        transactionMessage = "Withdrawal of " + TransferValue
                    };
                    _db.Transactions.Add(ta);
                    _db.Entry(ba).State = EntityState.Modified;
                    //_db.SaveChanges();
                    //return ba;

                }
                //ViewBag.Confirm = $"Your withdrawal of {TransferValue} was completed from account {TransferValue}";
                //return View("Confirmed");

                if (accountToType == "PersonalChecking")
                {
                    int accid = int.Parse(AccountID);
                    PersonalCheckingAccount personalCheckingAccount = _db.CheckingAccounts.Find(accid) as PersonalCheckingAccount;
                    double balance = personalCheckingAccount.Credit + double.Parse(TransferValue);
                    
                    personalCheckingAccount.Credit = balance;
                    Transaction ta = new Transaction()
                    {
                        id = 0,
                        accountID = int.Parse(AccountID),
                        transactionMessage = "Deposit of " + TransferValue
                    };
                    _db.Transactions.Add(ta);
                    _db.Entry(personalCheckingAccount).State = EntityState.Modified;
                    _db.SaveChanges();
                    ViewBag.Confirm = $"Your transfer of {TransferValue} was completed from account {FromAccID} to account {AccountID}";
                    return View("Confirmed");
                } else if (accountToType == "BusinessChecking")
                {
                    accID = int.Parse(AccountID);
                    ba = _db.BusinessAccounts.Find(accID) as BusinessCheckingAccount;

                    doubleCredit = ba.Credit;
                    double doubleDeposit = double.Parse(TransferValue);
                    doubleDebit = ba.Debit;
                    //double setAmount = doubleCredit - doubleDeposit;
                    

                    if (doubleCredit > 0)
                    {
                        setAmount = doubleCredit + doubleDeposit;
                        ba.Debit = 0;
                        ba.Credit = setAmount;
                        Transaction ta = new Transaction()
                        {
                            id = 0,
                            accountID = int.Parse(AccountID),
                            transactionMessage = "Deposit of " + TransferValue
                        };
                        _db.Transactions.Add(ta);
                        _db.Entry(ba).State = EntityState.Modified;
                        _db.SaveChanges();
                        ViewBag.Confirm = $"Your transfer of {TransferValue} was completed from account {FromAccID} to account {AccountID}";
                        return View("Confirmed");
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
                                accountID = int.Parse(AccountID),
                                transactionMessage = "Deposit of " + TransferValue
                            };
                            _db.Transactions.Add(ta);
                            _db.Entry(ba).State = EntityState.Modified;
                            _db.SaveChanges();
                            ViewBag.Confirm = $"Your transfer of {TransferValue} was completed from account {FromAccID} to account {AccountID}";
                            return View("Confirmed");
                        }
                        else
                        {
                            // when balance goes from debit to credit
                            ba.Credit = remainder;
                            ba.Debit = 0;
                            Transaction ta = new Transaction()
                            {
                                id = 0,
                                accountID = int.Parse(AccountID),
                                transactionMessage = "Deposit of " + TransferValue
                            };
                            _db.Transactions.Add(ta);
                            _db.Entry(ba).State = EntityState.Modified;
                            _db.SaveChanges();
                            ViewBag.Confirm = $"Your transfer of {TransferValue} was completed from account {FromAccID} to account {AccountID}";
                            return View("Confirmed");
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
                            accountID = int.Parse(AccountID),
                            transactionMessage = "Deposit of " + TransferValue
                        };
                        _db.Transactions.Add(ta);
                        _db.Entry(ba).State = EntityState.Modified;
                        _db.SaveChanges();
                        ViewBag.Confirm = $"Your transfer of {TransferValue} was completed from account {FromAccID} to account {AccountID}";
                        return View("Confirmed");
                    }

                    return View("Confirmed");

                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
            ViewBag.Confirm = $"Error";
            return View("Confirmed");
        }



        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            BusinessCheckingAccount personalCheckingAccount = _db.BusinessAccounts.Find(id);
            if (personalCheckingAccount == null)
            {
                return NotFound();
            }
            return View(personalCheckingAccount);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusinessCheckingAccount personalCheckingAccount = _db.BusinessAccounts.Find(id);
            _db.BusinessAccounts.Remove(personalCheckingAccount);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult Transactions(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var transactions = _db.Transactions.Where(t => t.accountID == id).ToList();
                ViewBag.MyList = transactions;
                return View(transactions);


            }


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
