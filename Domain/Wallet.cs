using Domain.DomaiinException;
using System;

namespace Domain
{
    public class Wallet : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string AccountNumber { get; set; }

        private decimal balance;
        public decimal Balance
        {
            get
            {
                return balance;
            }
            private set
            {
                if (value < 0)
                {
                    throw new Exception("value cannot be negative");
                }
                balance = GetBalance();
            }
        }

        private decimal GetBalance()
        {
            return balance;
        }

        /// <summary>
        /// Adding to the Balance
        /// 
        /// </summary>
        public decimal Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentIsOutOfRangeException("Amount to deposit must be positive and greater than 0");
            }
            balance = balance + amount;
            return balance;
        }

        /// <summary>
        /// Removing from the Balance
        /// </summary>
        public decimal Withdraw(decimal amount)
        {
            //remove from the balance
            if (amount <= 0)
            {
                throw new ArgumentIsOutOfRangeException("Amount to withdraw must be positive and greater than 0");
            }
            if (amount > balance)
            {
                throw new ArgumentIsOutOfRangeException("Amount to withdraw is more than balance");
            }
            balance = balance - amount;
            return balance;
        }
    }
}