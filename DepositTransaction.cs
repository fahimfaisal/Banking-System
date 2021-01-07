using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem

{
    class DepositTransaction : Transaction
    {


        private Account _account;
        new public decimal _amount;
        private bool _executed;
        new private bool _success;
        private bool _reversed;

        public DepositTransaction(Account account, decimal amount) : base(amount)
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
                Console.WriteLine("Amount Deposited to Account of " + _account.Name() + " is " + _amount.ToString("C"));
                Console.WriteLine(base.DateStamp);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("The Deposit was unsuccessful from the Account of " + _account.Name() + " for the amount of " + _amount.ToString("C"));
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
                    bool status = _account.Deposit((double)_amount);
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
                        bool with = _account.Withdraw((double)_amount);
                        if (with)
                        {
                            base.Execute();
                            this._reversed = true;
                            Console.WriteLine("The rollback on the selected Deposit Transaction is successful");
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
