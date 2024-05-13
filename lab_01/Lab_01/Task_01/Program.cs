using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using classes;

namespace Task_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            
            Money price1 = new Money(10, 50);
            Money price2 = new Money(5, 75);

            
            Product product1 = new Product("Яблуко", price1);
            Product product2 = new Product("Банан", price2);

            
            Console.WriteLine($"Назва: {product1.Name}, Ціна: {product1.Price.Dollars} грн {product1.Price.Cents} коп");
            Console.WriteLine($"Назва: {product2.Name}, Ціна: {product2.Price.Dollars} грн {product2.Price.Cents} коп");

            
            product1.DecreasePrice(2);

            
            Console.WriteLine($"Нова ціна для {product1.Name}: {product1.Price.Dollars} грн {product1.Price.Cents} коп");

            
            Money unitPrice = new Money(2, 0);
            Warehouse warehouseItem = new Warehouse("Яблука", "кг", unitPrice, 100, DateTime.Now);

            
            Console.WriteLine($"Товарний склад: {warehouseItem.Name}, Одиниця виміру: {warehouseItem.Unit}, Кількість: {warehouseItem.Quantity}");

            
            Reporting reporting = new Reporting();

            
            reporting.RegisterIncome(warehouseItem);

            
            reporting.InventoryReport();

            
            reporting.ShipItem(warehouseItem);

            
            reporting.InventoryReport();
        }
    }

}
