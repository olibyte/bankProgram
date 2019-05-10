using System;
using SplashKitSDK;
public class Account
{
    private decimal _balance;
    private string _name;

    public Account(string name, decimal startingBalance)
    {
        _name = name;
        _balance = startingBalance;
    }
    public bool Deposit(decimal amount)
    {
        if (amount > 0)
        {
            _balance += amount;
            return true;
        }
        return false;
    }
    public bool Withdraw(decimal amount)
    {
        if ((amount > 0) && (amount <= balance))
        {
            _balance -= amount;
            return true;
        }
        return false;
    }
    public string name
    {
        get { return _name; }
    }
    public decimal balance
    {
        get { return _balance; }
    }
    public void Print()
    {
        Console.WriteLine($"{name}'s account balance is {balance}. ");
    }
}