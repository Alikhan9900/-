using System;
using System.Collections.Generic;
using System.Text;

// Клас для грошей
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

// Клас для товарного складу
public class Warehouse
{
    public string Name { get; set; }
    public string Unit { get; set; }
    public Money UnitPrice { get; set; }
    public int Quantity { get; set; }
    public DateTime LastStockDate { get; set; }

    // Конструктор
    public Warehouse(string name, string unit, Money unitPrice, int quantity, DateTime lastStockDate)
    {
        Name = name;
        Unit = unit;
        UnitPrice = unitPrice;
        Quantity = quantity;
        LastStockDate = lastStockDate;
    }
}
// Клас для звітності
public class Reporting
{
    private List<Warehouse> warehouseItems;

    // Конструктор
    public Reporting()
    {
        warehouseItems = new List<Warehouse>();
    }

    // Метод для реєстрації надходження товару
    public void RegisterIncome(Warehouse item)
    {
        warehouseItems.Add(item);
        Console.WriteLine($"Зареєстровано надходження {item.Quantity} {item.Unit}(ів) товару {item.Name}.");
    }

    // Метод для відвантаження товару
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

    // Метод для звіту по інвентаризації
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

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;
        // Приклад використання класів і методів
        Money price1 = new Money(10, 50);
        Product product1 = new Product("Яблуко", price1);

        Money price2 = new Money(5, 75);
        Product product2 = new Product("Банан", price2);

        Money unitPrice = new Money(2, 0);
        Warehouse warehouseItem = new Warehouse("Яблука", "кг", unitPrice, 100, DateTime.Now);

        Reporting reporting = new Reporting();

        // Додамо товари до складу
        reporting.RegisterIncome(warehouseItem);

        // Реєстрація надходження товару
        reporting.RegisterIncome(warehouseItem);

        // Відвантаження товару
        reporting.ShipItem(warehouseItem);

        // Звіт по інвентаризації
        reporting.InventoryReport();

        // Вивід суми
        price1.DisplayAmount();
    }
}
