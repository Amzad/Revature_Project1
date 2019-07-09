﻿using System;

namespace Project1.Models
{
    public interface IAccount
    {
        String Withdraw(int accountID, double amount);
        String Deposit(int accountID, double amount);
        String Transfer(int fromAccount, int toAccount, double amount);
    }
}
