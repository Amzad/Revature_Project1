using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Project1.Models
{
    public class Context : DbContext
    {

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        //public virtual DbSet<Customer> GetCustomer { get; set; }
        //public virtual DbSet<Account> DeleteAccount { get; set; }

        public Context()
            : base("name = DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<Context>(null);
            base.OnModelCreating(modelBuilder);
        }
    }

}
