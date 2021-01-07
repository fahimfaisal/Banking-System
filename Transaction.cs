using System;
using System.Collections.Generic;
using System.Text;

namespace BankingSystem
{
    abstract class Transaction
    {
        protected decimal _amount;
        protected bool _success;
        private bool _executed;
        private bool _reversed;
        private DateTime _dateStamp;

        abstract public bool Success { get; }
       
    

        virtual public bool Executed
        {
            get
            {
                return _executed;
            }
        }

        virtual public bool Reversed
        {
            get
            {
                return _reversed;
            }
        }

        public DateTime DateStamp
        {
            get
            {
                return _dateStamp;
            }
        }

        public Transaction(decimal amount)
        {
            this._amount = amount;
        }

        public abstract void Print();

        public virtual void Execute()
        {
            _dateStamp = DateTime.Now;
            this._executed = true;
        }
        
        public virtual void Rollback()
        {
            _dateStamp = DateTime.Now;
           

            this._reversed = true;
            

        }
          
        


    }
}
