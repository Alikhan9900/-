using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

abstract class LightNode
{
    public abstract string OuterHTML { get; }
    public abstract string InnerHTML { get; }
}

class LightTextNode : LightNode
{
    public string Text { get; set; }

    public LightTextNode(string text)
    {
        Text = text;
    }

    public override string OuterHTML => Text;
    public override string InnerHTML => Text;
}

class LightElementNode : LightNode
{
    public string TagName { get; set; }
    public bool IsBlock { get; set; }
    public bool IsSelfClosing { get; set; }
    public List<string> CssClasses { get; set; }
    public List<LightNode> Children { get; set; }

    public LightElementNode(string tagName, bool isBlock = true, bool isSelfClosing = false)
    {
        TagName = tagName;
        IsBlock = isBlock;
        IsSelfClosing = isSelfClosing;
        CssClasses = new List<string>();
        Children = new List<LightNode>();
    }

    public void AddChild(LightNode child)
    {
        if (!IsSelfClosing)
        {
            Children.Add(child);
        }
    }

    public override string OuterHTML
    {
        get
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<{TagName}");

            if (CssClasses.Count > 0)
            {
                sb.Append($" class=\"{string.Join(" ", CssClasses)}\"");
            }

            if (IsSelfClosing)
            {
                sb.Append(" />");
            }
            else
            {
                sb.Append(">");
                foreach (var child in Children)
                {
                    sb.Append(child.OuterHTML);
                }
                sb.Append($"</{TagName}>");
            }

            return sb.ToString();
        }
    }

    public override string InnerHTML
    {
        get
        {
            if (IsSelfClosing)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            foreach (var child in Children)
            {
                sb.Append(child.InnerHTML);
            }
            return sb.ToString();
        }
    }
}

class FlyweightFactory
{
    private Dictionary<string, LightElementNode> elements = new Dictionary<string, LightElementNode>();

    public LightElementNode GetElement(string tagName, bool isBlock = true, bool isSelfClosing = false)
    {
        string key = $"{tagName}_{isBlock}_{isSelfClosing}";
        if (!elements.ContainsKey(key))
        {
            elements[key] = new LightElementNode(tagName, isBlock, isSelfClosing);
        }
        return elements[key];
    }
}

class Program
{
    static void Main(string[] args)
    {
        CreateTestBookFile();

        string[] bookLines = File.ReadAllLines("book.txt");

        FlyweightFactory factory = new FlyweightFactory();
        LightElementNode root = factory.GetElement("div", true, false);

        for (int i = 0; i < bookLines.Length; i++)
        {
            string line = bookLines[i];
            LightElementNode element;

            if (i == 0)
            {
                element = factory.GetElement("h1");
                element.AddChild(new LightTextNode(line));
            }
            else if (line.Length < 20)
            {
                element = factory.GetElement("h2");
                element.AddChild(new LightTextNode(line));
            }
            else if (line.StartsWith(" "))
            {
                element = factory.GetElement("blockquote");
                element.AddChild(new LightTextNode(line.Trim()));
            }
            else
            {
                element = factory.GetElement("p");
                element.AddChild(new LightTextNode(line));
            }

            root.AddChild(element);
        }

        Console.WriteLine("HTML Output:");
        Console.WriteLine(root.OuterHTML);

        long memoryUsed = GC.GetTotalMemory(true);
        Console.WriteLine($"\nMemory used: {memoryUsed} bytes");
    }

    static void CreateTestBookFile()
    {
        string[] bookLines = {
            "The Great Gatsby",
            "by F. Scott Fitzgerald",
            "",
            "In my younger and more vulnerable years my father gave me some advice that I’ve been turning over in my mind ever since.",
            " ",
            "“Whenever you feel like criticizing any one,” he told me, “just remember that all the people in this world haven’t had the advantages that you’ve had.”",
            " ",
            "He didn’t say any more but we’ve always been unusually communicative in a reserved way, and I understood that he meant a great deal more than that.",
            " "
        };

        File.WriteAllLines("book.txt", bookLines);
    }
}
