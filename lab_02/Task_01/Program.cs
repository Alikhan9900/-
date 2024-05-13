using System;
using System.Collections.Generic;

// Абстрактний клас підписки
public abstract class Subscription
{
    public double MonthlyFee { get; protected set; }
    public int MinimumSubscriptionPeriod { get; protected set; }
    public List<string> Channels { get; protected set; }

    public Subscription(double monthlyFee, int minPeriod, List<string> channels)
    {
        MonthlyFee = monthlyFee;
        MinimumSubscriptionPeriod = minPeriod;
        Channels = channels;
    }
}

// Конкретні класи підписок
public class DomesticSubscription : Subscription
{
    public DomesticSubscription() : base(10.99, 1, new List<string> { "Domestic News", "Domestic Sports" }) { }
}

public class EducationalSubscription : Subscription
{
    public EducationalSubscription() : base(15.99, 3, new List<string> { "Documentaries", "Educational Courses" }) { }
}

public class PremiumSubscription : Subscription
{
    public PremiumSubscription() : base(25.99, 6, new List<string> { "All Channels", "HD Streaming", "Exclusive Content" }) { }
}

// Фабричний метод
public interface ISubscriptionFactory
{
    Subscription CreateSubscription();
}

// Фабрики для кожного методу створення підписки
public class WebSite : ISubscriptionFactory
{
    public Subscription CreateSubscription()
    {
        // Логіка створення підписки через веб-сайт
        return new DomesticSubscription();
    }
}

public class MobileApp : ISubscriptionFactory
{
    public Subscription CreateSubscription()
    {
        // Логіка створення підписки через мобільний додаток
        return new PremiumSubscription();
    }
}

public class ManagerCall : ISubscriptionFactory
{
    public Subscription CreateSubscription()
    {
        // Логіка створення підписки через дзвінок менеджеру
        return new EducationalSubscription();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Приклад використання
        ISubscriptionFactory factory1 = new WebSite();
        Subscription subscription1 = factory1.CreateSubscription();
        Console.WriteLine("Subscription created via website:");
        Console.WriteLine($"Monthly Fee: {subscription1.MonthlyFee}$, Min Period: {subscription1.MinimumSubscriptionPeriod} months, Channels: {string.Join(", ", subscription1.Channels)}");

        ISubscriptionFactory factory2 = new MobileApp();
        Subscription subscription2 = factory2.CreateSubscription();
        Console.WriteLine("\nSubscription created via mobile app:");
        Console.WriteLine($"Monthly Fee: {subscription2.MonthlyFee}$, Min Period: {subscription2.MinimumSubscriptionPeriod} months, Channels: {string.Join(", ", subscription2.Channels)}");

        ISubscriptionFactory factory3 = new ManagerCall();
        Subscription subscription3 = factory3.CreateSubscription();
        Console.WriteLine("\nSubscription created via manager call:");
        Console.WriteLine($"Monthly Fee: {subscription3.MonthlyFee}$, Min Period: {subscription3.MinimumSubscriptionPeriod} months, Channels: {string.Join(", ", subscription3.Channels)}");
    }
}
