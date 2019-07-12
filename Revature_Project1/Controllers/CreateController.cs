using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Revature_Project1.Data;
using Revature_Project1.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Revature_Project1Controllers
{
    public class CreateController : Controller
    {
        private readonly MSSQLContext _db;

        public CreateController(MSSQLContext db)
        {
            _db = db;

        }

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public  ActionResult PCAccount()
        {
            return RedirectToAction("Index");
        }

        public ActionResult BCAccount()
        {
            return RedirectToAction("Index");
        }

        public ActionResult LAccount()
        {
            return RedirectToAction("Index");
        }

        public ActionResult TDAccount()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PCAccount(string accountType, string startingvalue)
        {
            //System.Diagnostics.Debug.WriteLine(accountType + " " + startingvalue);
            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            PersonalCheckingAccount pa = new PersonalCheckingBL().Create(accountType, startingvalue, userID);

            _db.CheckingAccounts.Add(pa);
            _db.SaveChanges();

            ViewBag.Message = "Congratulations! Your checking account has been created!";
            return View("Confirmed");
        }

        [HttpPost]
        public ActionResult BCAccount(string accountType, string startingvalue)
        {
            //System.Diagnostics.Debug.WriteLine(accountType + " " + startingvalue);
            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            BusinessCheckingAccount ba = new BusinessCheckingBL().Create(accountType, startingvalue, userID);

            _db.BusinessAccounts.Add(ba);
            _db.SaveChanges();

            ViewBag.Message = "Congratulations! Your business checking account has been created!";
            return View("Confirmed");
        }

        [HttpPost]
        public ActionResult LAccount(string accountType, string loanamount)
        {
            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            LoanAccount la = new LoanBL().Create(loanamount, userID);

            _db.LoanAccounts.Add(la);
            _db.SaveChanges();

            ViewBag.Message = "Congratulations! Your loan account has been created!";
            return View("Confirmed");
        }

        [HttpPost]
        public ActionResult TDAccount(string accountType, string startingvalue, string depositlength)
        {
            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            TermDepositAccount td = new TermDepositBL().Create(startingvalue, depositlength, userID);

            _db.TermDepositAccounts.Add(td);
            _db.SaveChanges();
            ViewBag.Message = "Congratulations! Your term deposit account has been created!";
            return View("Confirmed");
        }

        [HttpPost]
        public ActionResult Index(string account)
        {
            if (account == "pc") return View("PCAccount");
            if (account == "bc") return View("BCAccount");
            if (account == "la") return View("LAccount");
            if (account == "td") return View("TDAccount");

            return View("Eh");
        }

        [HttpGet]
        public ActionResult Confirmed()
        {
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult LoadMessage()
        {
            //var list = new PersonalCheckingBL().GetList();
            return PartialView("_MessageView");
        }

        [Authorize]
        public ActionResult LoadPC()
        {
            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = _db.CheckingAccounts.Where(c => c.customerID == userID).ToList();
            if (list.Count() > 0) return PartialView("_PCView", list);
            return null;
        }

        [Authorize]
        public ActionResult LoadBC()
        {
            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = _db.BusinessAccounts.Where(c => c.customerID == userID).ToList();
            if (list.Count() > 0) return PartialView("_BCView", list);
            return null;
        }

        [Authorize]
        public ActionResult LoadLA()
        {
            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = _db.LoanAccounts.Where(c => c.customerID == userID).ToList();
            if (list.Count() > 0) return PartialView("_LAView", list);
            return null;
        }

        [Authorize]
        public ActionResult LoadTD()
        {
            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = _db.TermDepositAccounts.Where(c => c.customerID == userID).ToList();
            if (list.Count() > 0) return PartialView("_TDView", list);
            return null;
        }
    }
}