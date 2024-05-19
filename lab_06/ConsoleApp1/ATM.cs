// src/ATM.cs
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace ATMProject
{
    public class ATM
    {
        private ATMState _currentState;
        private decimal _cashInMachine;
        private List<Account> _accounts;

        public ATM(decimal initialCash)
        {
            _cashInMachine = initialCash;
            _currentState = new NormalState(this);
            _accounts = new List<Account>();
        }

        public void SetState(ATMState state)
        {
            _currentState = state;
        }

        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        public void Deposit(Card card, decimal amount)
        {
            _currentState.Deposit(card, amount);
        }

        public void Withdraw(Card card, decimal amount)
        {
            _currentState.Withdraw(card, amount);
        }

        public void CheckBalance(Card card)
        {
            _currentState.CheckBalance(card);
        }

        public void Transfer(Card cardFrom, Card cardTo, decimal amount)
        {
            _currentState.Transfer(cardFrom, cardTo, amount);
        }

        public void DispenseCash(decimal amount)
        {
            if (amount <= _cashInMachine)
            {
                _cashInMachine -= amount;
                Console.WriteLine($"Dispensed {amount:C}");
            }
            else
            {
                Console.WriteLine("Insufficient funds in ATM.");
                SetState(new NoCashState(this));
            }
        }

        public decimal GetCashInMachine()
        {
            return _cashInMachine;
        }

        public Account GetAccount(Card card)
        {
            return _accounts.Find(a => a.Card.Equals(card));
        }
    }
}
