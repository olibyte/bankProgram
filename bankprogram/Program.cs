using System;
using SplashKitSDK;

public enum MenuOption
{
    Withdraw,
    Deposit,
    Transfer,
    NewAccount,
    Print,
    Quit
}
public class Program
{
    private static MenuOption ReadUserOption()
    {
        int option;
        Console.WriteLine("Please choose an item from the following menu\n1: Withdraw\n2: Deposit\n3: Transfer\n4: New Account\n5: Print \n6: Quit");
        do
        {
            Console.Write("Choose an option [1-6]: ");
            try
            {
                option = (Convert.ToInt32(Console.ReadLine()));
            }
            catch (Exception e)
            {
                Console.WriteLine("ReadUserOption Error! Please choose an option from the menu!");
                Console.WriteLine(e.Message);
                option = -1;
            }
            return (MenuOption)(option - 1);
        } while ((option < 1) || (option > 6));
    }
    private static void DoDeposit(Bank toBank)
    {
        Account toAccount = FindAccount(toBank);
        if (toAccount == null) return;

        try
        {
            decimal amount;
            Console.Write("Please enter the amount to deposit: ");

            amount = Convert.ToDecimal(Console.ReadLine());

            DepositTransaction deposit = new DepositTransaction(toAccount, amount);

            Bank.ExecuteTransaction(deposit);

        }
        catch (Exception e)
        {
            Console.WriteLine("Deposit error!");
            Console.WriteLine(e.Message);
        }
    }
    private static void DoWithdraw(Bank fromBank)
    {
        Account fromAccount = FindAccount(fromBank);
        if (fromAccount == null) return;

        decimal amount;
        Console.Write("Please enter the amount to withdraw: ");
        try
        {
            amount = Convert.ToDecimal(Console.ReadLine());

            WithdrawTransaction withdraw = new WithdrawTransaction(fromAccount, amount);

            Bank.ExecuteTransaction(withdraw);
        }
        catch (Exception e)
        {
            Console.WriteLine("Withdrawal error!");
            Console.WriteLine(e.Message);
        }
    }
    private static void DoTransfer(Bank fromBank, Bank toBank)
    {

        Account fromAccount = FindAccount(fromBank);
        if (fromAccount == null) return;
        Console.WriteLine($"Transferring From {fromAccount.name}");
        Account toAccount = FindAccount(toBank);
        if (toAccount == null) return;
        Console.WriteLine($"Transferring to {toAccount.name}");

        decimal amount;
        Console.Write($"Please enter the amount to transfer to {toAccount.name}: ");
        try
        {
            amount = Convert.ToDecimal(Console.ReadLine());

            TransferTransaction transfer = new TransferTransaction(fromAccount, toAccount, amount);

            Bank.ExecuteTransaction(transfer);
        }
        catch (Exception e)
        {
            Console.WriteLine("Transfer error!");
            Console.WriteLine(e.Message);
        }
    }
    private static void DoPrint(Bank fromBank)
    {
        Account fromAccount = FindAccount(fromBank);
        if (fromAccount == null) return;

        Console.WriteLine($"{fromAccount.name}'s account balance is {fromAccount.balance}");
    }
    private static void DoNewAccount()
    {
        decimal startingBalance;
        string name;
        Console.Write("Please enter the new account name: ");

        name = Convert.ToString(Console.ReadLine());

        Console.Write("Please enter the initial balance: ");

        startingBalance = Convert.ToDecimal(Console.ReadLine());

        Account account = new Account(name, startingBalance);
        Bank.AddAcount(account);
    }
    private static Account FindAccount(Bank fromBank)
    {
        Console.Write("Enter account name: ");
        String name = Console.ReadLine();
        Account result = fromBank.GetAccount(name);

        if (result == null)
        {
            Console.WriteLine($"No account found with name {name}");
        }
        return result;
    }
    public static void Main()
    {
        MenuOption userSelection;

        Bank toBank = new Bank();
        Bank fromBank = new Bank();
        do
        {
            userSelection = ReadUserOption();

            switch (userSelection)
            {
                case MenuOption.Withdraw:
                    DoWithdraw(fromBank);
                    break;
                case MenuOption.Deposit:
                    DoDeposit(toBank);
                    break;
                case MenuOption.Transfer:
                    DoTransfer(fromBank, toBank);
                    break;
                case MenuOption.NewAccount:
                    DoNewAccount();
                    break;
                case MenuOption.Print:
                    DoPrint(fromBank);
                    // Console.WriteLine("TBC");
                    break;
                case MenuOption.Quit:
                    Console.WriteLine("Soiya!");
                    break;
                default:
                    break;
            }

        } while (userSelection != MenuOption.Quit);
    }
}