using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BankingSystem
{
    class TransferTransaction : Transaction
    {
        private Account _fromAccount;
        private Account _toAccount;
        new private decimal _amount;
        private DepositTransaction _deposit;
        private WithdrawTransaction _withdraw;
        private bool _executed;
        private bool _reversed;
        
        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount): base(amount)
        {
            this._fromAccount = fromAccount;
            this._toAccount = toAccount;
            this._amount = amount;
        }


        public override bool Success
        {
            get
            {
                if (_deposit.Success && _withdraw.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override void Print()
        {
            if (Success)
            {
                
                Console.WriteLine("The amount of money Transfered:" + this._amount.ToString("C"));
                Console.WriteLine("From Account of: " + _fromAccount.Name());
                Console.WriteLine("To The Account of: " + _toAccount.Name());
                Console.WriteLine(base.DateStamp);
                Console.WriteLine();

            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("The Transfer was unsuccessful from the Account of " + _fromAccount.Name() + " to the Account of " + _toAccount.Name() +" for the amount of " + _amount.ToString("C"));

                Console.WriteLine(base.DateStamp);
                Console.WriteLine();
            }

        }

        public override void Execute()
        {
            try
            {
                base.Execute();
                if (_executed)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    this._executed = true;

                    _withdraw = new WithdrawTransaction(this._fromAccount, this._amount);
                    _deposit = new DepositTransaction(this._toAccount, this._amount);

                    _withdraw.Execute();

                    if (_withdraw.Success)
                    {
                        _deposit.Execute();


                        if (_deposit.Success)
                        {

                            this._reversed = false;


                        }
                        else
                        {

                            _withdraw.Rollback();
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




            public override void  Rollback()
            {
                try
                {
                    
                    if (this._reversed == false && this._executed == true)
                    {
                        _deposit.Rollback();
                        _withdraw.Rollback();

                        if (Success)
                        {
                            base.Execute();
                            this._reversed = true;
                            Console.WriteLine("The Rollback on the Selected Transfer Transaction is Successful");
                            Console.WriteLine(base.DateStamp);
                        }
                        else
                        {
                       
                            throw new InvalidOperationException();
                        }

                    }
                    else
                    {
                        Console.WriteLine("The Rollback was already Executed on " + base.DateStamp);
                        throw new InvalidOperationException();
                    }
                }
                catch (InvalidOperationException e)
                {

                    Console.WriteLine(e.Message.ToString());

                }
            }




        }
    }

