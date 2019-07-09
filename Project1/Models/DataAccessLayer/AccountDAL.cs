using System.Collections.Generic;
namespace Project1.Models
{
    public class AccountDAL
    {
        public Account GetAccount(int accountID)
        {
            //return accountList.Find(acc => acc.AccountID == accountID);
            return null;
        }

        public List<Account> GetAllCustAccounts(int custID)
        {
            //List<Account> customerAccountList = accountList.FindAll(acc => acc.customerID == custID);

            //return customerAccountList;
            return null;
        }

        public Account deleteCustomerAccount(int accID)
        {
            /*Account acc = accountList.Find(acc2 => acc2.AccountID == accID);
            accountList.Remove(acc);
            return acc;*/
            return null;
        }

        public List<string> getTransactionLog(Account account)
        {
            //return account.transactionLog;
            return null;
        }
    }
}
