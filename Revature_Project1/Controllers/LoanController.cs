using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Revature_Project1.Data;
using Revature_Project1.Models;

namespace Revature_Project1.Controllers
{
    public class LoanController : Controller
    {
        private readonly MSSQLContext _db;

        public LoanController(MSSQLContext db)
        {
            _db = db;

        }

        public ActionResult Pay(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            LoanAccount la = _db.LoanAccounts.Find(id);
            if (la == null)
            {
                return NotFound();
            }
            return View(la);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pay(string accountID, string Debit, string paymentvalue)
        {
            try
            {
                double balance = double.Parse(Debit) - double.Parse(paymentvalue);
                int accid = int.Parse(accountID);
                LoanAccount la = _db.LoanAccounts.Find(accid) as LoanAccount;
                la.Debit = balance;
                Transaction ta = new Transaction()
                {
                    id = 0,
                    accountID = int.Parse(accountID),
                    transactionMessage = "Deposit of " + paymentvalue
                };
                _db.Transactions.Add(ta);
                _db.Entry(la).State = EntityState.Modified;
                _db.SaveChanges();
                ViewBag.Confirm = $"Your payment of {paymentvalue} was completed for account {accountID}";
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
            return RedirectToAction("Index");
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
