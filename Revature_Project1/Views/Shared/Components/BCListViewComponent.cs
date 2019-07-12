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
    public class BCListViewComponent : ViewComponent
    {
        private readonly MSSQLContext _db;

        public BCListViewComponent(MSSQLContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(
        int maxPriority, bool isDone)
        {
            var items = await GetItemsAsync();
            return View(items);
        }
        private Task<List<BusinessCheckingAccount>> GetItemsAsync()
        {
            var userID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _db.BusinessAccounts.Where(c => c.customerID == userID).ToListAsync();
;
        }
    }
}
