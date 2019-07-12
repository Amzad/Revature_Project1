using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Revature_Project1.Data;
using Revature_Project1.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Revature_Project1.Views.Shared.Components
{
    public class PPCTListViewComponent : ViewComponent
    {
        private readonly MSSQLContext _db;

        public PPCTListViewComponent(MSSQLContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(
        string currentID, string creditFrom, string transfervalue)
        {
            System.Diagnostics.Debug.WriteLine(currentID);
            var items = await GetItemsAsync();
            int id = int.Parse(currentID);
            ViewBag.FromAccID = currentID;
            ViewBag.CreditFrom = creditFrom;
            ViewBag.TransferValue = transfervalue;
            return View(items);
        }
        private Task<List<PersonalCheckingAccount>> GetItemsAsync()
        {
            var userID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _db.CheckingAccounts.Where(c => c.customerID == userID).ToListAsync();
;
        }
    }
}
