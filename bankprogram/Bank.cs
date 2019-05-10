using System;
using SplashKitSDK;
using System.Collections.Generic;

public class Bank
{
    private static List<Account> _accounts = new List<Account>();
    public static void AddAcount(Account account)
    {
        _accounts.Add(account);
    }
    public Account GetAccount(string name)
    {
        foreach (Account account in _accounts)
        {
            if (account.name == name){
                return account;
            }
        }
        return null;
    }
    public static void ExecuteTransaction(WithdrawTransaction transaction)
    {
        transaction.Execute();
        transaction.Print();
    }
    public static void ExecuteTransaction(DepositTransaction transaction)
    {
        transaction.Execute();
        transaction.Print();
    }
    public static void ExecuteTransaction(TransferTransaction transaction)
    {
        transaction.Execute();
        transaction.Print();
    }
}