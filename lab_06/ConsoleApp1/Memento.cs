// src/Memento.cs
namespace ATMProject
{
    public class Memento
    {
        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; }

        public Memento(string accountNumber, decimal balance)
        {
            AccountNumber = accountNumber;
            Balance = balance;
        }
    }
}
