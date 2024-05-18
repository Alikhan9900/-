using System;
using System.Collections.Generic;

// Інтерфейс спостерігача
interface IEventListener
{
    void Update(string eventType, LightElementNode element);
}
