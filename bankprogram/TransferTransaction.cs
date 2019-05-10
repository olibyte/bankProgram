using System;
using SplashKitSDK;

public class TransferTransaction
{
    private Account _fromAccount;
    private Account _toAccount;
    private WithdrawTransaction _theWithdraw;
    private DepositTransaction _theDeposit;
    private decimal _amount;
    private bool _executed = false;

    private bool _reversed = false;

    public bool Success { get { return (_theWithdraw.Success && _theDeposit.Success); } }
    public bool Executed { get { return _executed; } }
    public bool Reversed { get { return _reversed; } }

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        _amount = amount;
        _theWithdraw = new WithdrawTransaction(fromAccount, amount);
        _theDeposit = new DepositTransaction(toAccount, amount);
    }
    public void Execute()
    {
        try
        {
            if (Executed)
            {
                throw new Exception("Cannot execute this transaction as it has already been performed.");
            }
            _executed = true;
            _theWithdraw.Execute();
            if (_theWithdraw.Success)
            {
                _theDeposit.Execute();
            }
            else if (!_theDeposit.Success)
            {
                _theDeposit.Rollback();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Transfer execute error! " + e.Message);
        }
    }
    public void Rollback()
    {
        try
        {
            if (!Executed)
            {
                throw new Exception("Transaction has not been executed.");
            }
            if (!Reversed)
            {
                throw new Exception("Transaction has been reversed.");
            }
            if (_theWithdraw.Success)
            {
                _theWithdraw.Rollback();
            }
            if (_theDeposit.Success)
            {
                _theDeposit.Rollback();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Transfer rollback error! " + e.Message);
        }
    }
    public void Print()
    {
        try
        {
            if (Success && Executed)
            {
                Console.WriteLine($"Transfer of {_amount} from {_fromAccount.name} to {_toAccount.name} successful!");
            }
            if (Reversed)
            {
                Console.WriteLine("Transfer reversed");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Transfer print error! " + e.Message);
        }
    }
}