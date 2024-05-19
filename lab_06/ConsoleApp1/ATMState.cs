// src/ATMState.cs
using System;

namespace ATMProject
{
    public abstract class ATMState
    {
        protected ATM _atm;

        protected ATMState(ATM atm)
        {
            _atm = atm;
        }

        public abstract void Deposit(Card card, decimal amount);
        public abstract void Withdraw(Card card, decimal amount);
        public abstract void CheckBalance(Card card);
        public abstract void Transfer(Card cardFrom, Card cardTo, decimal amount);
    }

    // src/NormalState.cs
    public class NormalState : ATMState
    {
        public NormalState(ATM atm) : base(atm) { }

        public override void Deposit(Card card, decimal amount)
        {
            var account = _atm.GetAccount(card);
            if (account != null)
            {
                account.Deposit(amount);
                Console.WriteLine($"Deposited {amount:C} to account {account.AccountNumber}");
            }
        }

        public override void Withdraw(Card card, decimal amount)
        {
            var account = _atm.GetAccount(card);
            if (account != null && account.Balance >= amount)
            {
                account.Withdraw(amount);
                _atm.DispenseCash(amount);
                Console.WriteLine($"Withdrew {amount:C} from account {account.AccountNumber}");
            }
            else
            {
                Console.WriteLine("Insufficient funds in account.");
            }
        }

        public override void CheckBalance(Card card)
        {
            var account = _atm.GetAccount(card);
            if (account != null)
            {
                Console.WriteLine($"Account {account.AccountNumber} balance: {account.Balance:C}");
            }
        }

        public override void Transfer(Card cardFrom, Card cardTo, decimal amount)
        {
            var accountFrom = _atm.GetAccount(cardFrom);
            var accountTo = _atm.GetAccount(cardTo);

            if (accountFrom != null && accountTo != null && accountFrom.Balance >= amount)
            {
                accountFrom.Withdraw(amount);
                accountTo.Deposit(amount);
                Console.WriteLine($"Transferred {amount:C} from account {accountFrom.AccountNumber} to account {accountTo.AccountNumber}");
            }
            else
            {
                Console.WriteLine("Transfer failed. Check the accounts and balance.");
            }
        }
    }

    // src/OutOfServiceState.cs
    public class OutOfServiceState : ATMState
    {
        public OutOfServiceState(ATM atm) : base(atm) { }

        public override void Deposit(Card card, decimal amount)
        {
            Console.WriteLine("ATM is out of service.");
        }

        public override void Withdraw(Card card, decimal amount)
        {
            Console.WriteLine("ATM is out of service.");
        }

        public override void CheckBalance(Card card)
        {
            Console.WriteLine("ATM is out of service.");
        }

        public override void Transfer(Card cardFrom, Card cardTo, decimal amount)
        {
            Console.WriteLine("ATM is out of service.");
        }
    }

    // src/NoCashState.cs
    public class NoCashState : ATMState
    {
        public NoCashState(ATM atm) : base(atm) { }

        public override void Deposit(Card card, decimal amount)
        {
            var account = _atm.GetAccount(card);
            if (account != null)
            {
                account.Deposit(amount);
                Console.WriteLine($"Deposited {amount:C} to account {account.AccountNumber}");
            }
        }

        public override void Withdraw(Card card, decimal amount)
        {
            Console.WriteLine("ATM has no cash to dispense.");
        }

        public override void CheckBalance(Card card)
        {
            var account = _atm.GetAccount(card);
            if (account != null)
            {
                Console.WriteLine($"Account {account.AccountNumber} balance: {account.Balance:C}");
            }
        }

        public override void Transfer(Card cardFrom, Card cardTo, decimal amount)
        {
            var accountFrom = _atm.GetAccount(cardFrom);
            var accountTo = _atm.GetAccount(cardTo);

            if (accountFrom != null && accountTo != null && accountFrom.Balance >= amount)
            {
                accountFrom.Withdraw(amount);
                accountTo.Deposit(amount);
                Console.WriteLine($"Transferred {amount:C} from account {accountFrom.AccountNumber} to account {accountTo.AccountNumber}");
            }
            else
            {
                Console.WriteLine("Transfer failed. Check the accounts and balance.");
            }
        }
    }
}
