using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;

namespace BankingSystem

{
    class Bank
    {

        public List<Account> accounts = new List<Account>();

        public List<Transaction> transactions = new List<Transaction>();








        public Bank()
        {

        }
        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);

        }
        public void AddAccount(Account account)
        {
            accounts.Add(account);
        }

        public Account GetAccount(string name)
        {
            string cap = name.ToUpper();
            foreach (Account account in accounts)
            {
                string cap2 = account._name.ToUpper();
                if (cap.Contains(cap2))
                {
                    return account;
                }
            }

            return null;


        }
    
        public void ExecuteTransaction(Transaction transaction)
        {
            
                transaction.Execute();
                AddTransaction(transaction);
           
            
        }
        
        public void RollbackTransaction(Transaction transaction)
        {
            transaction.Rollback();

            
        }
       
        public void PrintTransactionHistory()
        {
            int i = 1;
            foreach(Transaction transaction in transactions)
            {
               
                    
                    Console.Write(i + ".");
                    transaction.Print();
                    i++;
                
               
            }
               
        }
    }
}
