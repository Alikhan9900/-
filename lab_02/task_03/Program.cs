using System;

public class Authenticator
{
    // Приватне статичне поле для зберігання єдиного екземпляра класу
    private static Authenticator instance;

    // Приватний конструктор, щоб заборонити зовнішній доступ до створення нових екземплярів
    private Authenticator()
    {
        // Код ініціалізації, якщо потрібно
    }

    // Публічний статичний метод для отримання єдиного екземпляра класу
    public static Authenticator GetInstance()
    {
        // Перевірка, чи є вже створений екземпляр, якщо ні, то створюємо
        if (instance == null)
        {
            instance = new Authenticator();
        }

        return instance;
    }

    public void Authenticate(string username, string password)
    {
        // Метод аутентифікації
        Console.WriteLine($"Authenticating {username} with password {password}...");
        // Логіка аутентифікації
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Спроба створити екземпляри класу Authenticator
        Authenticator auth1 = Authenticator.GetInstance();
        Authenticator auth2 = Authenticator.GetInstance();

        // Перевірка, чи це один і той же екземпляр
        Console.WriteLine($"auth1 == auth2: {auth1 == auth2}"); // Повинно вивести true

        // Приклад використання аутентифікатора
        auth1.Authenticate("user1", "password1");
        auth2.Authenticate("user2", "password2");
    }
}

