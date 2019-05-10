using System;
using SplashKitSDK;

public class DepositTransaction
{
    private Account _account;
    private decimal _amount;

    private bool _success = false;

    private bool _executed = false;

    private bool _reversed = false;

    public bool Success { get { return _success; } }
    public bool Executed { get { return _executed; } }
    public bool Reversed { get { return _reversed; } }
    public DepositTransaction(Account account, decimal amount)
    {
        _account = account;
        _amount = amount;
    }
    public void Execute()
    {
        if (_executed)
        {
            throw new Exception("Cannot execute this transaction as it has already been performed.");
        }
        _executed = true;
        _success = _account.Deposit(_amount);
    }
    public void Rollback()
    {
        if (!Executed)
        {
            throw new Exception("Transaction has not been executed.");
        }
        if (Reversed)
        {
            throw new Exception("Transaction has been reversed.");
        }
        if ((Success) && (!Reversed))
        {
            _executed = false;
            _success = false;
            _reversed = true;
            _account.Withdraw(_amount);
        }
    }
    public void Print()
    {
        try
        {
            if (Success && Executed)
            {
                Console.WriteLine($"Deposit of {_amount} successful!");
            }
            if (Reversed)
            {
                Console.WriteLine("Deposit reversed");
            }
            else if (!Success)
            {
                Console.WriteLine("Else error: Insufficient deposit.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Deposit Error: {e.Message}");
        }
    }
}
