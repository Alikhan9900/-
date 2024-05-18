using System;
using System.IO;
using System.Text.RegularExpressions;

class SmartTextReader
{
    private string filePath;

    public SmartTextReader(string filePath)
    {
        this.filePath = filePath;
    }

    public char[][] ReadFile()
    {
        string[] lines = File.ReadAllLines(filePath);
        char[][] result = new char[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            result[i] = lines[i].ToCharArray();
        }

        return result;
    }
}

class SmartTextChecker : SmartTextReader
{
    private SmartTextReader reader;

    public SmartTextChecker(SmartTextReader reader) : base(reader.GetType().GetField("filePath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(reader).ToString())
    {
        this.reader = reader;
    }

    public new char[][] ReadFile()
    {
        Console.WriteLine("Opening file...");

        char[][] result = reader.ReadFile();

        Console.WriteLine("File read successfully.");
        Console.WriteLine("Closing file...");

        int lineCount = result.Length;
        int charCount = 0;
        foreach (var line in result)
        {
            charCount += line.Length;
        }

        Console.WriteLine($"Total lines: {lineCount}");
        Console.WriteLine($"Total characters: {charCount}");

        return result;
    }
}

class SmartTextReaderLocker : SmartTextReader
{
    private SmartTextReader reader;
    private Regex filePattern;

    public SmartTextReaderLocker(SmartTextReader reader, string pattern) : base(reader.GetType().GetField("filePath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(reader).ToString())
    {
        this.reader = reader;
        this.filePattern = new Regex(pattern);
    }

    public new char[][] ReadFile()
    {
        string filePath = reader.GetType().GetField("filePath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(reader).ToString();

        if (filePattern.IsMatch(filePath))
        {
            Console.WriteLine("Access denied!");
            return null;
        }

        return reader.ReadFile();
    }
}

class Program
{
    static void Main(string[] args)
    {
        string filePath = "example.txt";
        string restrictedFilePath = "restricted_example.txt";

        // Create files with random content
        CreateRandomTextFile(filePath);
        CreateRandomTextFile(restrictedFilePath);

        // Creating the original SmartTextReader
        SmartTextReader reader = new SmartTextReader(filePath);

        // Using SmartTextChecker to log file operations
        SmartTextChecker checker = new SmartTextChecker(reader);
        char[][] text = checker.ReadFile();

        if (text != null)
        {
            Console.WriteLine("Content:");
            foreach (var line in text)
            {
                Console.WriteLine(new string(line));
            }
        }

        // Using SmartTextReaderLocker to limit access to files
        string restrictedPattern = @"^restricted_.*\.txt$";
        SmartTextReaderLocker locker = new SmartTextReaderLocker(reader, restrictedPattern);

        // Attempt to read an unrestricted file
        text = locker.ReadFile();

        // Attempt to read a restricted file
        SmartTextReader restrictedReader = new SmartTextReader(restrictedFilePath);
        SmartTextReaderLocker restrictedLocker = new SmartTextReaderLocker(restrictedReader, restrictedPattern);
        text = restrictedLocker.ReadFile();
    }

    static void CreateRandomTextFile(string filePath)
    {
        Random random = new Random();
        int linesCount = random.Next(5, 11);
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < linesCount; i++)
            {
                writer.WriteLine(GenerateRandomString(random.Next(20, 51), random));
            }
        }
    }

    static string GenerateRandomString(int length, Random random)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        char[] stringChars = new char[length];

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(stringChars);
    }
}
