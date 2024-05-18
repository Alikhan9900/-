using System;

class EventListener : IEventListener
{
    private string name;

    public EventListener(string name)
    {
        this.name = name;
    }

    public void Update(string eventType, LightElementNode element)
    {
        Console.WriteLine($"{name} received {eventType} event from <{element.TagName}>");
    }
}
