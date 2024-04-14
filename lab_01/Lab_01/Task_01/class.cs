using System;
using System.Collections.Generic;
using System.Text;

// Клас для грошей
namespace classes
{
    public class Money
    {
        public int Dollars { get; set; } // Ціла частина
        public int Cents { get; set; }   // Дробова частина

        // Конструктор
        public Money(int dollars, int cents)
        {
            Dollars = dollars;
            Cents = cents;
        }

        // Метод для виведення суми на екран
        public void DisplayAmount()
        {
            Console.WriteLine($"Сума: {Dollars} грн {Cents} коп");
        }
    }

    // Клас для продукту
    public class Product
    {
        public string Name { get; set; }
        public Money Price { get; set; }

        // Конструктор
        public Product(string name, Money price)
        {
            Name = name;
            Price = price;
        }

        // Метод для зменшення ціни
        public void DecreasePrice(int amount)
        {
            Price.Cents -= amount;
            if (Price.Cents < 0)
            {
                Price.Dollars--;
                Price.Cents += 100;
            }
        }
    }


    public class Warehouse
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public Money UnitPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime LastStockDate { get; set; }


        public Warehouse(string name, string unit, Money unitPrice, int quantity, DateTime lastStockDate)
        {
            Name = name;
            Unit = unit;
            UnitPrice = unitPrice;
            Quantity = quantity;
            LastStockDate = lastStockDate;
        }
    }

    public class Reporting
    {
        private List<Warehouse> warehouseItems;


        public Reporting()
        {
            warehouseItems = new List<Warehouse>();
        }


        public void RegisterIncome(Warehouse item)
        {
            warehouseItems.Add(item);
            Console.WriteLine($"Зареєстровано надходження {item.Quantity} {item.Unit}(ів) товару {item.Name}.");
        }


        public void ShipItem(Warehouse item)
        {
            if (warehouseItems.Contains(item))
            {
                warehouseItems.Remove(item);
                Console.WriteLine($"Відвантажено {item.Quantity} {item.Unit}(ів) товару {item.Name}.");
            }
            else
            {
                Console.WriteLine("Товар не знайдено на складі.");
            }
        }


        public void InventoryReport()
        {
            Console.WriteLine("Звіт по інвентаризації:");
            if (warehouseItems.Count == 0)
            {
                Console.WriteLine("На складі немає товарів.");
            }
            else
            {
                foreach (var item in warehouseItems)
                {
                    Console.WriteLine($"- {item.Name}: {item.Quantity} {item.Unit}(ів)");
                }
            }
        }
    }
}
