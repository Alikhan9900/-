using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

// Інтерфейс стратегії завантаження зображення
interface IImageLoadingStrategy
{
    Task<byte[]> LoadImage(string href);
}

// Стратегія для завантаження зображення з файлової системи
class FileSystemImageLoadingStrategy : IImageLoadingStrategy
{
    public Task<byte[]> LoadImage(string href)
    {
        return Task.Run(() =>
        {
            if (File.Exists(href))
            {
                return File.ReadAllBytes(href);
            }
            else
            {
                throw new FileNotFoundException($"File {href} not found.");
            }
        });
    }
}

// Стратегія для завантаження зображення з мережі
class NetworkImageLoadingStrategy : IImageLoadingStrategy
{
    private static readonly HttpClient httpClient = new HttpClient();

    public async Task<byte[]> LoadImage(string href)
    {
        HttpResponseMessage response = await httpClient.GetAsync(href);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsByteArrayAsync();
    }
}

// Базовий клас LightNode
abstract class LightNode
{
    public abstract string OuterHTML { get; }
    public abstract string InnerHTML { get; }
}

// Клас для елементів LightElementNode
class LightElementNode : LightNode
{
    public string TagName { get; set; }
    public bool IsBlock { get; set; }
    public bool IsSelfClosing { get; set; }
    public List<string> CssClasses { get; set; }
    public List<LightNode> Children { get; set; }
    private Dictionary<string, List<IEventListener>> eventListeners;

    public LightElementNode(string tagName, bool isBlock = true, bool isSelfClosing = false)
    {
        TagName = tagName;
        IsBlock = isBlock;
        IsSelfClosing = isSelfClosing;
        CssClasses = new List<string>();
        Children = new List<LightNode>();
        eventListeners = new Dictionary<string, List<IEventListener>>();
    }

    public void AddChild(LightNode child)
    {
        if (!IsSelfClosing)
        {
            Children.Add(child);
        }
    }

    public void AddEventListener(string eventType, IEventListener listener)
    {
        if (!eventListeners.ContainsKey(eventType))
        {
            eventListeners[eventType] = new List<IEventListener>();
        }
        eventListeners[eventType].Add(listener);
    }

    public void RemoveEventListener(string eventType, IEventListener listener)
    {
        if (eventListeners.ContainsKey(eventType))
        {
            eventListeners[eventType].Remove(listener);
        }
    }

    public void NotifyEvent(string eventType)
    {
        if (eventListeners.ContainsKey(eventType))
        {
            foreach (var listener in eventListeners[eventType])
            {
                listener.Update(eventType, this);
            }
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

// Клас для текстових вузлів
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

// Інтерфейс для обробників подій
interface IEventListener
{
    void Update(string eventType, LightElementNode element);
}

// Реалізація обробника подій
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

// Клас для вузла зображення
class LightImageNode : LightElementNode
{
    private string href;
    private IImageLoadingStrategy loadingStrategy;
    private byte[] imageData;

    public LightImageNode(string href) : base("img", false, true)
    {
        this.href = href;
        DetermineStrategy(href);
    }

    private void DetermineStrategy(string href)
    {
        if (Uri.IsWellFormedUriString(href, UriKind.Absolute) &&
            (href.StartsWith("http://") || href.StartsWith("https://")))
        {
            loadingStrategy = new NetworkImageLoadingStrategy();
        }
        else
        {
            loadingStrategy = new FileSystemImageLoadingStrategy();
        }
    }

    public async Task LoadImageAsync()
    {
        imageData = await loadingStrategy.LoadImage(href);
    }

    public override string OuterHTML
    {
        get
        {
            return $"<img src=\"{href}\" />";
        }
    }
}

// Головний клас програми
class Program
{
    static async Task Main(string[] args)
    {
        // Створення тестового файлу зображення
        CreateTestImageFile();

        // Завантаження зображення з файлової системи
        LightImageNode fileImage = new LightImageNode("test_image.jpg");
        await fileImage.LoadImageAsync();
        Console.WriteLine(fileImage.OuterHTML);

        // Завантаження зображення з мережі
        LightImageNode networkImage = new LightImageNode("https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.work.ua%2Fru%2Farticles%2Fjobseeker%2F579%2F&psig=AOvVaw2-R2YF9iI4BXARtEHrM_pG&ust=1716115986968000&source=images&cd=vfe&opi=89978449&ved=0CBIQjRxqFwoTCOCu0LGEl4YDFQAAAAAdAAAAABAE");
        await networkImage.LoadImageAsync();
        Console.WriteLine(networkImage.OuterHTML);
    }

    static void CreateTestImageFile()
    {
        byte[] imageData = new byte[100]; // Створення пустого зображення
        new Random().NextBytes(imageData); // Заповнення випадковими байтами
        File.WriteAllBytes("test_image.jpg", imageData);
    }
}
