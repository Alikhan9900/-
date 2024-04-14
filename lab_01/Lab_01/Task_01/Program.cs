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
            // Створюємо гроші
            Money price1 = new Money(10, 50);
            Money price2 = new Money(5, 75);

            // Створюємо товари
            Product product1 = new Product("Яблуко", price1);
            Product product2 = new Product("Банан", price2);

            // Відображаємо інформацію про товари
            Console.WriteLine($"Назва: {product1.Name}, Ціна: {product1.Price.Dollars} грн {product1.Price.Cents} коп");
            Console.WriteLine($"Назва: {product2.Name}, Ціна: {product2.Price.Dollars} грн {product2.Price.Cents} коп");

            // Зменшуємо ціну першого товару
            product1.DecreasePrice(2);

            // Відображаємо оновлену ціну першого товару
            Console.WriteLine($"Нова ціна для {product1.Name}: {product1.Price.Dollars} грн {product1.Price.Cents} коп");

            // Створюємо товарний склад
            Money unitPrice = new Money(2, 0);
            Warehouse warehouseItem = new Warehouse("Яблука", "кг", unitPrice, 100, DateTime.Now);

            // Відображаємо інформацію про товарний склад
            Console.WriteLine($"Товарний склад: {warehouseItem.Name}, Одиниця виміру: {warehouseItem.Unit}, Кількість: {warehouseItem.Quantity}");

            // Створюємо екземпляр класу звітності
            Reporting reporting = new Reporting();

            // Реєстрація надходження товару на склад
            reporting.RegisterIncome(warehouseItem);

            // Відображення звіту по інвентаризації
            reporting.InventoryReport();

            // Відвантаження товару зі складу
            reporting.ShipItem(warehouseItem);

            // Відображення звіту по інвентаризації
            reporting.InventoryReport();
        }
    }

}
