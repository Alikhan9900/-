using System;

abstract class SupportHandler
{
    protected SupportHandler nextHandler;

    public void SetNextHandler(SupportHandler handler)
    {
        nextHandler = handler;
    }

    public abstract void HandleRequest(string request);
}
