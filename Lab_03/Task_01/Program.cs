using System;

class Program
{
    static void Main(string[] args)
    {
        Logger consoleLogger = new Logger();
        consoleLogger.Log("This is a log message.");
        consoleLogger.Warn("This is a warning message.");
        consoleLogger.Error("This is an error message.");

        string filePath = "log.txt";
        FileLoggerAdapter fileLogger = new FileLoggerAdapter(filePath);
        fileLogger.Log("This is a log message.");
        fileLogger.Warn("This is a warning message.");
        fileLogger.Error("This is an error message.");

        Console.WriteLine($"Messages have been logged to the console and to the file '{filePath}'.");
    }
}
