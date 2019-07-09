using System;
using System.Collections.Generic;
namespace Project1.Models

{
	public class CustomerDAL
	{

        public static List<Customer> customerList = new List<Customer>();

        public Account addAccount(Customer cust, Account account)
        {
            //cust.Accounts.Add(account.AccountID);
            //return account;
            return null;
        }

		public Customer Get(int custID)
		{
            try
            {
                //Customer cust = customerList.Find(c => c.ID == custID);
                //return cust;
            }
            catch (Exception)
            {
                throw;
            }
            return null;

		}

        public int deleteAccount(int custID, int accID)
        {
            foreach(Customer customer in customerList)
            {
               // if (customer.Accounts.Remove(accID)) return accID;
            }

            return 0;
        }
	}
}
