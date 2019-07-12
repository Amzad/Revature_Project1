using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Revature_Project1.Data;

[assembly: HostingStartup(typeof(Revature_Project1.Areas.Identity.IdentityHostingStartup))]
namespace Revature_Project1.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddIdentity<MSSQLUser, IdentityRole>()
                     .AddEntityFrameworkStores<MSSQLContext>().AddDefaultUI(UIFramework.Bootstrap4);
            });
        }
    }
}