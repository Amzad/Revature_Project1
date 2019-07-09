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

        [HttpPost]
        public ActionResult PCAccount(string accountType, string startingvalue)
        {
            System.Diagnostics.Debug.WriteLine(accountType + " " + startingvalue);
            if (accountType == "Personal Checking Account") new PersonalCheckingBL().Create(accountType, startingvalue);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string account)
        {
            if (account == "pc") return View("PCAccount");
            if (account == "bc") return View("Blah");
            if (account == "la") return View("Blah");
            if (account == "td") return View("Blah");

            return View("Eh");
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}