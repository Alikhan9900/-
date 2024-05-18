using System;

class Program
{
    static void Main(string[] args)
    {
        // Створення ланцюжка відповідальностей
        SupportHandler basicSupport = new BasicSupportHandler();
        SupportHandler technicalSupport = new TechnicalSupportHandler();
        SupportHandler advancedSupport = new AdvancedSupportHandler();
        SupportHandler supervisorSupport = new SupervisorSupportHandler();

        basicSupport.SetNextHandler(technicalSupport);
        technicalSupport.SetNextHandler(advancedSupport);
        advancedSupport.SetNextHandler(supervisorSupport);

        while (true)
        {
            // Виведення меню
            Console.WriteLine("Welcome to the support system. Please choose an option:");
            Console.WriteLine("1. Basic Support");
            Console.WriteLine("2. Technical Support");
            Console.WriteLine("3. Advanced Support");
            Console.WriteLine("4. Supervisor Support");
            Console.WriteLine("5. Exit");

            string userInput = Console.ReadLine();

            if (userInput == "5")
            {
                break;
            }

            basicSupport.HandleRequest(userInput);
            Console.WriteLine();
        }
    }
}
