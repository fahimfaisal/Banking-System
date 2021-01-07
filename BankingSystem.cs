using System;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks.Dataflow;

namespace BankingSystem

{
    enum MenuOption
    {
        Add_Account,
        Withdraw,
        Deposit,
        Transaction_History,
        Print,
        Transfer,
        Quit

    }


    class BankSystem
    {
        static void Main(string[] args)
        {
            static MenuOption ReadUserOption()                    // Function to read the user input and return it.
            {
                Console.WriteLine("1." + MenuOption.Add_Account);
                Console.WriteLine("2." + MenuOption.Withdraw);
                Console.WriteLine("3." + MenuOption.Deposit);
                Console.WriteLine("4." + MenuOption.Transaction_History);
                Console.WriteLine("5." + MenuOption.Print);
                Console.WriteLine("6." + MenuOption.Transfer);
                Console.WriteLine("7." + MenuOption.Quit);

                MenuOption nest;

                Console.WriteLine("Choose an option");

                try
                {


                    do
                    {


                        int opt = Convert.ToInt32(Console.ReadLine());


                        if (opt < 8 && opt > 0)
                        {
                            opt -= 1;
                            nest = (MenuOption)opt;
                            Console.WriteLine("Are you sure you want to " + nest + " [Y/N]");
                            string ans = Console.ReadLine();
                            Console.WriteLine();
                            if (ans == "Y" || ans == "y")
                            {
                                return nest;

                            }

                        }
                        else
                        {
                            Console.WriteLine("Please Choose a valid option");
                        }

                    } while (true);


                }
                catch (System.FormatException e)
                {
                    Console.WriteLine(e.Message.ToString());
                    return (MenuOption) 7;
                }

            }
            

            bool play = true;
            while (play)
            {
                MenuOption noas = ReadUserOption();
                switch (noas)
                {
                    case MenuOption.Add_Account:
                        NewAccount();
                        break;
                    case MenuOption.Withdraw:
                        DoWithdraw();
                        break;
                    case MenuOption.Deposit:
                        DoDeposit();
                        break;
                    case MenuOption.Transaction_History:
                        DoRollback();
                        break;
                    case MenuOption.Print:
                        DoPrint();
                        break;
                    case MenuOption.Transfer:
                        DoTransfer();
                        break;
                    case MenuOption.Quit:
                        Console.WriteLine("quit");
                        play = false;
                        break;

                    default:
                        break;
                }

                Console.WriteLine();
            }

        }

        static Bank newBank = new Bank();
        static void DoWithdraw()
        {
            DoPrint();
            Console.WriteLine("Enter name of an account to withdraw");
            string name = Console.ReadLine();
            
            Account select = newBank.GetAccount(name);

            Console.WriteLine("Enter the amount to withdraw");
            
            double amount = Convert.ToInt32(Console.ReadLine());
            try
            {
                WithdrawTransaction with = new WithdrawTransaction(select, (decimal)amount);
                newBank.ExecuteTransaction(with);
                Console.WriteLine();
                with.Print();
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine();
                Console.WriteLine("No Account Found in this name");
                Console.WriteLine("Thrown Exception: "+e.Message.ToString());
            }

         


        }

        static void DoDeposit()
        {
            DoPrint();
            Console.WriteLine("Enter name of an account to Deposit");
            string name = Console.ReadLine();

            Account select = newBank.GetAccount(name);

            Console.WriteLine("Enter the amount to Deposit");

            double amount = Convert.ToInt32(Console.ReadLine());
            try
            {
                DepositTransaction depo = new DepositTransaction(select, (decimal)amount);
                newBank.ExecuteTransaction(depo);
                Console.WriteLine();
                depo.Print();
            }
            catch(NullReferenceException e)
            {
                Console.WriteLine();
                Console.WriteLine("No Account Found in this name");
                Console.WriteLine("Thrown Exception: " + e.Message.ToString());
            }
            
        }

        static void DoTransfer()
        {
            DoPrint();
            Console.WriteLine("Enter the name of the sender account");
            string send = Console.ReadLine();

            Console.WriteLine("Enter the name of the receiving account");
            string receive = Console.ReadLine();

            Account sendSelect = newBank.GetAccount(send);

            Account receiveSelect = newBank.GetAccount(receive);

            Console.WriteLine("Enter the amount to Transfer");

            double amount = Convert.ToInt32(Console.ReadLine());
            try
            {
                TransferTransaction transfer = new TransferTransaction(sendSelect, receiveSelect, (decimal)amount);
                newBank.ExecuteTransaction(transfer);
                Console.WriteLine();
                transfer.Print();
            }
            catch(NullReferenceException e)
            {
                Console.WriteLine();
                Console.WriteLine("No Account Found in this name");
                Console.WriteLine("Thrown Exception: " + e.Message.ToString());
            }
        }

        

        static void DoPrint()
        {
            foreach(Account account in newBank.accounts)
            {
                account.Print();
            }
        }

        static void DoRollback()
        {
            newBank.PrintTransactionHistory();
            
            try
            {
                if (newBank.transactions.Count == 0)
                {
                    Console.WriteLine("There is No Transaction Recorded.");
                }
                else
                {

                    Console.WriteLine("Choose a Transaction to Rollback ");
                    int ans = Convert.ToInt32(Console.ReadLine());
                    int last = ans - 1;
                    Transaction bank = newBank.transactions.ElementAt(last);
                    newBank.RollbackTransaction(bank);

                }
                
            }
        
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine();
                Console.WriteLine("Please select correct index of transaction");
                Console.WriteLine(e.Message.ToString());
            }
            
        }
        
        static void NewAccount()
        {
            try
            {
                Console.WriteLine("Enter name of the Account holder");
                string name =Console.ReadLine();
                Console.WriteLine("Enter bank opening balance");
                double balance = Convert.ToDouble(Console.ReadLine());

                Account newAcc = new Account(name, balance);

                newBank.AddAccount(newAcc);
            }
            catch(System.FormatException e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            


        }

        public static Account FindAccount(Bank bank)
        {
            
            
                Console.WriteLine("Enter the name of the  bank Account");

                string input = Console.ReadLine();

                Account getAcc = bank.GetAccount(input);
                getAcc.Print();

                return getAcc;
            
            
           
                
        }


    }
}


