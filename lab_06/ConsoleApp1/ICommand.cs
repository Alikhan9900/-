// src/ICommand.cs
namespace ATMProject
{
    public interface ICommand
    {
        void Execute();
    }
}

// src/DepositCommand.cs
namespace ATMProject
{
    public class DepositCommand : ICommand
    {
        private readonly ATM _atm;
        private readonly Card _card;
        private readonly decimal _amount;

        public DepositCommand(ATM atm, Card card, decimal amount)
        {
            _atm = atm;
            _card = card;
            _amount = amount;
        }

        public void Execute()
        {
            _atm.Deposit(_card, _amount);
        }
    }
}

// src/WithdrawCommand.cs
namespace ATMProject
{
    public class WithdrawCommand : ICommand
    {
        private readonly ATM _atm;
        private readonly Card _card;
        private readonly decimal _amount;

        public WithdrawCommand(ATM atm, Card card, decimal amount)
        {
            _atm = atm;
            _card = card;
            _amount = amount;
        }

        public void Execute()
        {
            _atm.Withdraw(_card, _amount);
        }
    }
}

// src/TransferCommand.cs
namespace ATMProject
{
    public class TransferCommand : ICommand
    {
        private readonly ATM _atm;
        private readonly Card _cardFrom;
        private readonly Card _cardTo;
        private readonly decimal _amount;

        public TransferCommand(ATM atm, Card cardFrom, Card cardTo, decimal amount)
        {
            _atm = atm;
            _cardFrom = cardFrom;
            _cardTo = cardTo;
            _amount = amount;
        }

        public void Execute()
        {
            _atm.Transfer(_cardFrom, _cardTo, _amount);
        }
    }
}

// src/BalanceCheckCommand.cs
namespace ATMProject
{
    public class BalanceCheckCommand : ICommand
    {
        private readonly ATM _atm;
        private readonly Card _card;

        public BalanceCheckCommand(ATM atm, Card card)
        {
            _atm = atm;
            _card = card;
        }

        public void Execute()
        {
            _atm.CheckBalance(_card);
        }
    }
}
