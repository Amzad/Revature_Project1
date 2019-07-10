using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project1.Models;

namespace Project1.Controllers
{
    public class CreateController : Controller
    {
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
            if (accountType == "Personal Checking Account") new PersonalCheckingBL().Create(accountType, startingvalue);
            ViewBag.Message = "Congratulations! Your checking account has been created!";
            return View("Confirmed");
        }

        [HttpPost]
        public ActionResult BCAccount(string accountType, string startingvalue)
        {
            //System.Diagnostics.Debug.WriteLine(accountType + " " + startingvalue);
            if (accountType == "Business Checking Account") new BusinessCheckingBL().Create(accountType, startingvalue);
            ViewBag.Message = "Congratulations! Your checking account has been created!";
            return View("Confirmed");
        }

        [HttpPost]
        public ActionResult LAccount(string accountType, string loanamount)
        {
            //System.Diagnostics.Debug.WriteLine(accountType + " " + loanamount);
            if (accountType == "Loan Account") new LoanBL().Create(loanamount);
            ViewBag.Message = "Congratulations! Your loan account has been created!";
            return View("Confirmed");
        }

        [HttpPost]
        public ActionResult TDAccount(string accountType, string startingvalue, string depositlength)
        {
            //System.Diagnostics.Debug.WriteLine(accountType + " " + startingvalue);
            if (accountType == "Term Deposit Account") new TermDepositBL().Create(startingvalue, depositlength);
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
            var list = new PersonalCheckingBL().GetList();
            return PartialView("_PCView", list);
        }

        [Authorize]
        public ActionResult LoadBC()
        {
            var list = new BusinessCheckingBL().GetList();
            return PartialView("_BCView", list);
        }

        [Authorize]
        public ActionResult LoadLA()
        {
            var list = new LoanBL().GetList();
            return PartialView("_LAView", list);
        }

        [Authorize]
        public ActionResult LoadTD()
        {
            var list = new TermDepositBL().GetList();
            return PartialView("_TDView", list);
        }
    }
}