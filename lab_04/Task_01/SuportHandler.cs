using System;

class BasicSupportHandler : SupportHandler
{
    public override void HandleRequest(string request)
    {
        if (request == "1")
        {
            Console.WriteLine("Basic Support: We can help with basic account and billing questions.");
        }
        else if (nextHandler != null)
        {
            nextHandler.HandleRequest(request);
        }
    }
}

class TechnicalSupportHandler : SupportHandler
{
    public override void HandleRequest(string request)
    {
        if (request == "2")
        {
            Console.WriteLine("Technical Support: We can help with technical issues and troubleshooting.");
        }
        else if (nextHandler != null)
        {
            nextHandler.HandleRequest(request);
        }
    }
}

class AdvancedSupportHandler : SupportHandler
{
    public override void HandleRequest(string request)
    {
        if (request == "3")
        {
            Console.WriteLine("Advanced Support: We can help with advanced issues and service interruptions.");
        }
        else if (nextHandler != null)
        {
            nextHandler.HandleRequest(request);
        }
    }
}

class SupervisorSupportHandler : SupportHandler
{
    public override void HandleRequest(string request)
    {
        if (request == "4")
        {
            Console.WriteLine("Supervisor Support: We can escalate your issue to a supervisor.");
        }
        else
        {
            Console.WriteLine("Invalid choice. Please try again.");
            if (nextHandler != null)
            {
                nextHandler.HandleRequest(request);
            }
        }
    }
}
