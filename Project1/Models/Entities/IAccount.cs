using System;

namespace Project1.Models
{
    public interface IAccount
    {
        String Deposit(int accountID, double amount);
        String Transfer(int fromAccount, int toAccount, double amount);
    }
}
