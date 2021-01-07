using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BankingSystem
{
    class WithdrawTransaction : Transaction
    {
        private Account _account;
        new private decimal _amount;
        private bool _executed;
        new private bool _success;
        private bool _reversed;

        public WithdrawTransaction(Account account, decimal amount): base(amount)
        {
            this._account = account;
            this._amount = amount;
        }

        public override bool Success
        {
            get
            {
                return this._success;
            }
        }

        public override bool Executed
        {
            get
            {
                return this._executed;
            }
        }

        public override bool Reversed
        {
            get
            {
                return this._reversed;
            }
        }

        public override void Print()
        {
            if (Success)
            {
               
                
                Console.WriteLine("The transaction was successful");
                Console.WriteLine("Amount Withdrawn From Account of "+_account.Name()+ " is " + _amount.ToString("C"));
                Console.WriteLine(base.DateStamp);
                Console.WriteLine();

            }
            else
            {
                
                Console.WriteLine("The withdraw was unsuccessful from the Account of "+_account.Name()+" for the amount of " + _amount.ToString("C"));
                Console.WriteLine(base.DateStamp);
                Console.WriteLine();
            }
        }

        public override void Execute()
        {
            try
            {
                base.Execute();
                if (Executed)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    bool status = _account.Withdraw((double)_amount);
                    this._executed = true;

                    if (status)
                    {
                        this._success = true;
                        this._reversed = false;
                       
                    }
                    else

                    {
                        throw new InvalidOperationException();

                    }
                }
                    

                

            }

            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            
        }

        public override void Rollback()
        {
            try
            {
               
                if (Reversed)
                {
                    Console.WriteLine("The Rollback was previously Executed on " + base.DateStamp);
                    throw new InvalidOperationException();
                }
                else
                {
                    if (Success)
                    {
                        base.Execute();
                        bool dep = _account.Deposit((double)_amount);
                        if (dep)
                        {
                            this._reversed = true;
                            Console.WriteLine("The Rollback for the Selected Withdraw Transaction is Successful");
                            Console.WriteLine(base.DateStamp);
                        }

                        else
                        {
                            throw new InvalidOperationException();
                        }
                    }

                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
               
            }
            catch (InvalidOperationException e)
            {


                Console.WriteLine(e.Message.ToString()); 
            }
            


        }

    }

}
    


