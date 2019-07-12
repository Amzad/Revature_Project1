using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Revature_Project1.Models;

namespace Revature_Project1.Data

{
    public class MSSQLContext : IdentityDbContext<MSSQLUser>
    {

        public DbSet<PersonalCheckingAccount> CheckingAccounts { get; set; }
        public DbSet<BusinessCheckingAccount> BusinessAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<LoanAccount> LoanAccounts { get; set; }
        public DbSet<TermDepositAccount> TermDepositAccounts { get; set; }

        public MSSQLContext(DbContextOptions<MSSQLContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
