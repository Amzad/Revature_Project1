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
    public class TermDepositController : Controller
    {
        private readonly MSSQLContext _db;

        public TermDepositController(MSSQLContext db)
        {
            _db = db;

        }

        public ActionResult Withdraw(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TermDepositAccount la = _db.TermDepositAccounts.Find(id);
            if (la == null)
            {
                return NotFound();
            }
            return View(la);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Withdraw(string accountID, string Credit, string withdrawvalue, string depositTerm)
        {
            try
            {
                if (int.Parse(depositTerm) > 0)
                {
                    ViewBag.Confirm = $"The term deposit has not matured yet.";
                    return View("Confirmed");
                }
                double balance = double.Parse(Credit) - double.Parse(withdrawvalue);
                int accid = int.Parse(accountID);
                TermDepositAccount la = _db.TermDepositAccounts.Find(accid) as TermDepositAccount;
                la.Credit = balance;
                Transaction ta = new Transaction()
                {
                    id = 0,
                    accountID = int.Parse(accountID),
                    transactionMessage = "Deposit of " + withdrawvalue
                };
                _db.Transactions.Add(ta);
                _db.Entry(la).State = EntityState.Modified;
                _db.SaveChanges();
                ViewBag.Confirm = $"Your withdrawal of {withdrawvalue} was completed for account {accountID}";
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
