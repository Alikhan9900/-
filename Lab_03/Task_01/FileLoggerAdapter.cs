class FileLoggerAdapter : Logger
{
    private FileWriter fileWriter;

    public FileLoggerAdapter(string path)
    {
        fileWriter = new FileWriter(path);
    }

    public new void Log(string message)
    {
        fileWriter.WriteLine("LOG: " + message);
    }

    public new void Error(string message)
    {
        fileWriter.WriteLine("ERROR: " + message);
    }

    public new void Warn(string message)
    {
        fileWriter.WriteLine("WARN: " + message);
    }
}
