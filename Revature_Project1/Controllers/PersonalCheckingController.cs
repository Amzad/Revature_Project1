using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Revature_Project1.Data;
using Revature_Project1.Models;

namespace Revature_Project1.Controllers
{
    public class PersonalCheckingController : Controller
    {
        private readonly MSSQLContext _db;

        public PersonalCheckingController(MSSQLContext db)
        {
            _db = db;

        }

        public ActionResult Index()
        {
            return View(_db.CheckingAccounts.ToList());
        }

        public ActionResult Withdraw(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PersonalCheckingAccount personalCheckingAccount = _db.CheckingAccounts.Find(id);

            if (personalCheckingAccount == null)
            {
                return NotFound();
            }
            return View(personalCheckingAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Withdraw(string accountID, string Credit, string withdrawvalue)
        {
            try
            {
                int accid = int.Parse(accountID);
                double balance = double.Parse(Credit) - double.Parse(withdrawvalue);
                ;
                if (balance >= 0)
                {
                    PersonalCheckingAccount personalCheckingAccount = _db.CheckingAccounts.Find(accid) as PersonalCheckingAccount;
                    personalCheckingAccount.Credit = balance;
                    Transaction ta = new Transaction()
                    {
                        id = 0,
                        accountID = int.Parse(accountID),
                        transactionMessage = "Withdrawal of " + withdrawvalue
                    };
                    _db.Transactions.Add(ta);
                    //_db.Entry(personalCheckingAccount).State = EntityState.Modified;
                    _db.Entry(personalCheckingAccount).State = EntityState.Modified;
                    _db.SaveChanges();
                    ViewBag.Confirm = $"Your withdrawal of {withdrawvalue} was completed from account {accountID}.";
                    return View("Confirmed");
                }
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }

        public ActionResult TransferFrom(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PersonalCheckingAccount personalCheckingAccount = _db.CheckingAccounts.Find(id);

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
            PersonalCheckingAccount personalCheckingAccount = _db.CheckingAccounts.Find(id);

            if (personalCheckingAccount == null)
            {
                return NotFound();
            }
            return View(personalCheckingAccount);
        }


        public ActionResult Transfer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PersonalCheckingAccount personalCheckingAccount = _db.CheckingAccounts.Find(id);

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
            System.Diagnostics.Debug.WriteLine(FromAccID);
            System.Diagnostics.Debug.WriteLine(CreditFrom);
            System.Diagnostics.Debug.WriteLine(TransferValue);
            System.Diagnostics.Debug.WriteLine(AccountID);
            System.Diagnostics.Debug.WriteLine(accountToType);
            try
            {
                int accidFrom = int.Parse(FromAccID);
                int accidTo = int.Parse(AccountID);
                double balance = double.Parse(CreditFrom) - double.Parse(TransferValue);


                if (balance >= 0)
                {
                    PersonalCheckingAccount personalCheckingAccount = _db.CheckingAccounts.Find(accidFrom) as PersonalCheckingAccount;
                    personalCheckingAccount.Credit = balance;
                    Transaction ta = new Transaction()
                    {
                        id = 0,
                        accountID = int.Parse(AccountID),
                        transactionMessage = "Withdrawal of " + TransferValue
                    };
                    _db.Transactions.Add(ta);
                    _db.Entry(personalCheckingAccount).State = EntityState.Modified;

                    if (accountToType == "PersonalChecking")
                    {
                        PersonalCheckingAccount pa = _db.CheckingAccounts.Find(accidTo) as PersonalCheckingAccount;
                        pa.Credit = pa.Credit + double.Parse(TransferValue);
                        Transaction ta2 = new Transaction()
                        {
                            id = 0,
                            accountID = int.Parse(AccountID),
                            transactionMessage = "Deposit of " + TransferValue
                        };
                        _db.Transactions.Add(ta2);
                        _db.Entry(pa).State = EntityState.Modified;
                        _db.SaveChanges();
                        ViewBag.Confirm = $"Your transfer of {TransferValue} was completed from account {FromAccID} to account {AccountID}";
                        return View("Confirmed");
                    }
                    else if (accountToType == "BusinessChecking")
                    {
                        BusinessCheckingAccount ba = _db.BusinessAccounts.Find(accidTo) as BusinessCheckingAccount;
                        double doubleCredit = ba.Credit;
                        double doubleDeposit = double.Parse(TransferValue);
                        double doubleDebit = ba.Debit;
                        int accID = int.Parse(AccountID);


                        if (doubleCredit > 0)
                        {
                            double setAmount = doubleCredit + doubleDeposit;
                            ba.Debit = 0;
                            ba.Credit = setAmount;
                            Transaction ta2 = new Transaction()
                            {
                                id = 0,
                                accountID = int.Parse(AccountID),
                                transactionMessage = "Deposit of " + TransferValue
                            };
                            _db.Transactions.Add(ta2);
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
                                Transaction ta2 = new Transaction()
                                {
                                    id = 0,
                                    accountID = int.Parse(AccountID),
                                    transactionMessage = "Deposit of " + TransferValue
                                };
                                _db.Transactions.Add(ta2);
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
                                Transaction ta2 = new Transaction()
                                {
                                    id = 0,
                                    accountID = int.Parse(AccountID),
                                    transactionMessage = "Deposit of " + TransferValue
                                };
                                _db.Transactions.Add(ta2);
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
                            Transaction ta2 = new Transaction()
                            {
                                id = 0,
                                accountID = int.Parse(AccountID),
                                transactionMessage = "Deposit of " + TransferValue
                            };
                            _db.Transactions.Add(ta2);
                            _db.Entry(ba).State = EntityState.Modified;
                            _db.SaveChanges();
                            ViewBag.Confirm = $"Your transfer of {TransferValue} was completed from account {FromAccID} to account {AccountID}";
                            return View("Confirmed");
                        }
                        return View("Confirmed");
                    }
                    else if (accountToType == "Loan")
                    {
                        return View("Confirmed");
                    }
                }
                else
                {
                    ViewBag.Confirm = $"Your account balance cannot be overdrafted. Transaction canceled.";
                    return View("Confirmed");



                }
                return View("Confirmed");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }



        public ActionResult Deposit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PersonalCheckingAccount personalCheckingAccount = _db.CheckingAccounts.Find(id) as PersonalCheckingAccount;
            if (personalCheckingAccount == null)
            {
                return NotFound();
            }
            return View(personalCheckingAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit(string accountID, string Credit, string depositvalue)
        {
            try
            {
                double balance = double.Parse(Credit) + double.Parse(depositvalue);
                int accid = int.Parse(accountID);
                PersonalCheckingAccount personalCheckingAccount = _db.CheckingAccounts.Find(accid) as PersonalCheckingAccount;
                personalCheckingAccount.Credit = balance;
                Transaction ta = new Transaction()
                {
                    id = 0,
                    accountID = int.Parse(accountID),
                    transactionMessage = "Deposit of " + depositvalue
                };
                _db.Transactions.Add(ta);
                _db.Entry(personalCheckingAccount).State = EntityState.Modified;
                _db.SaveChanges();
                ViewBag.Confirm = $"Your deposit of {depositvalue} was completed from account {accountID}";
                return View("Confirmed");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PersonalCheckingAccount personalCheckingAccount = _db.CheckingAccounts.Find(id);
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
            PersonalCheckingAccount personalCheckingAccount = _db.CheckingAccounts.Find(id);
            _db.CheckingAccounts.Remove(personalCheckingAccount);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult Transactions(int? id)
        {
            if (id == null)
            {
                return NotFound();
            } else
            {
                var transactions =_db.Transactions.Where(t => t.accountID == id).ToList();
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
