using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;



namespace BankingSystem

{
    
    class Account
    {
         public string _name;   //Name of the bank account
         public double _balance;  // Balance of the bank account


        public Account(string name, double balance)       //Constructor method
        {
            this._name = name;
            this._balance = balance;
        }

        public bool Deposit(double amount) // Method to deposit money in the account
        {
            if (amount < 0)
            {
                Console.WriteLine("You can't add negative amount");
                return false;
            }

            else
            {
                this._balance += amount;
                
                return true;
            }
        }

        public bool Withdraw(double amount) //Method to withdraw money from the account
        {
            if (amount < 0 || amount > this._balance)
            {
                Console.WriteLine("Invalid amount to withdraw");

                return false;
               

            }

            else
            {
                this._balance -= amount;
                


                return true;
            }
        }

        public void Print()  //Method to print the account name and balance in the console
        {
            Console.WriteLine();
            Console.WriteLine("Name of the Account holder: " + Name() + "\nAccount Balance: " + this._balance.ToString("C"));
            Console.WriteLine();
        }

        public string Name()
        {
            return this._name;
        }

        
    }
}
