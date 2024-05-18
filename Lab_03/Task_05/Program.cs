using System;

class Program
{
    static void Main(string[] args)
    {
        // Створення текстових вузлів
        LightTextNode textNode1 = new LightTextNode("Hello, world!");
        LightTextNode textNode2 = new LightTextNode("This is a custom markup language.");

        // Створення елементів
        LightElementNode div = new LightElementNode("div", isBlock: true);
        div.CssClasses.Add("container");

        LightElementNode span = new LightElementNode("span", isBlock: false);
        span.AddChild(textNode1);

        LightElementNode paragraph = new LightElementNode("p", isBlock: true);
        paragraph.AddChild(textNode2);

        div.AddChild(span);
        div.AddChild(paragraph);

        // Виведення outerHTML і innerHTML
        Console.WriteLine("Outer HTML:");
        Console.WriteLine(div.OuterHTML);

        Console.WriteLine("\nInner HTML:");
        Console.WriteLine(div.InnerHTML);
    }
}
