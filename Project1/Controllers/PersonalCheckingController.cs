using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project1.Models;

namespace Project1.Controllers
{
    public class PersonalCheckingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PersonalCheckingAccounts
        public ActionResult Index()
        {
            return View(db.CheckingAccounts.ToList());
        }

        // GET: PersonalCheckingAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalCheckingAccount personalCheckingAccount = db.CheckingAccounts.Find(id);
            if (personalCheckingAccount == null)
            {
                return HttpNotFound();
            }
            return View(personalCheckingAccount);
        }

        // GET: PersonalCheckingAccounts/Edit/5
        public ActionResult Withdraw(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalCheckingAccount personalCheckingAccount = db.CheckingAccounts.Find(id);
            if (personalCheckingAccount == null)
            {
                return HttpNotFound();
            }
            return View(personalCheckingAccount);
        }

        // POST: PersonalCheckingAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Withdraw(string accountID, string Credit, string withdrawvalue)
        {
            //System.Diagnostics.Debug.WriteLine(accountID);
            //System.Diagnostics.Debug.WriteLine(Credit);
            //System.Diagnostics.Debug.WriteLine(withdrawvalue);
            try
            {
                PersonalCheckingAccount pa = new PersonalCheckingBL().Withdraw(accountID, Credit, withdrawvalue);
                ViewBag.Confirm = $"Your withdrawal of {withdrawvalue} was completed from account {accountID}";
                return View("Confirmed");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        /*public ActionResult Withdraw(String completed)
        {
           // return
        }*/

        // GET: PersonalCheckingAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalCheckingAccount personalCheckingAccount = db.CheckingAccounts.Find(id);
            if (personalCheckingAccount == null)
            {
                return HttpNotFound();
            }
            return View(personalCheckingAccount);
        }

        // POST: PersonalCheckingAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonalCheckingAccount personalCheckingAccount = db.CheckingAccounts.Find(id);
            db.CheckingAccounts.Remove(personalCheckingAccount);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
