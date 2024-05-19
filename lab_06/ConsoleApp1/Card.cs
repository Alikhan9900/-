// src/Card.cs
namespace ATMProject
{
    public class Card
    {
        public string CardNumber { get; private set; }

        public Card(string cardNumber)
        {
            CardNumber = cardNumber;
        }
    }
}
