using System;

// Абстрактні класи для різних видів техніки
public abstract class Laptop
{
    public abstract void DisplayInfo();
}

public abstract class Netbook
{
    public abstract void DisplayInfo();
}

public abstract class EBook
{
    public abstract void DisplayInfo();
}

public abstract class Smartphone
{
    public abstract void DisplayInfo();
}

// Конкретні класи для різних брендів техніки
public class IProneLaptop : Laptop
{
    public override void DisplayInfo()
    {
        Console.WriteLine("This is an IProne Laptop.");
    }
}

public class IProneNetbook : Netbook
{
    public override void DisplayInfo()
    {
        Console.WriteLine("This is an IProne Netbook.");
    }
}

public class IProneEBook : EBook
{
    public override void DisplayInfo()
    {
        Console.WriteLine("This is an IProne EBook.");
    }
}

public class IProneSmartphone : Smartphone
{
    public override void DisplayInfo()
    {
        Console.WriteLine("This is an IProne Smartphone.");
    }
}

public class KiaomiLaptop : Laptop
{
    public override void DisplayInfo()
    {
        Console.WriteLine("This is a Kiaomi Laptop.");
    }
}

public class KiaomiNetbook : Netbook
{
    public override void DisplayInfo()
    {
        Console.WriteLine("This is a Kiaomi Netbook.");
    }
}

public class KiaomiEBook : EBook
{
    public override void DisplayInfo()
    {
        Console.WriteLine("This is a Kiaomi EBook.");
    }
}

public class KiaomiSmartphone : Smartphone
{
    public override void DisplayInfo()
    {
        Console.WriteLine("This is a Kiaomi Smartphone.");
    }
}

public class BalaxyLaptop : Laptop
{
    public override void DisplayInfo()
    {
        Console.WriteLine("This is a Balaxy Laptop.");
    }
}

public class BalaxyNetbook : Netbook
{
    public override void DisplayInfo()
    {
        Console.WriteLine("This is a Balaxy Netbook.");
    }
}

public class BalaxyEBook : EBook
{
    public override void DisplayInfo()
    {
        Console.WriteLine("This is a Balaxy EBook.");
    }
}

public class BalaxySmartphone : Smartphone
{
    public override void DisplayInfo()
    {
        Console.WriteLine("This is a Balaxy Smartphone.");
    }
}

// Абстрактна фабрика
public abstract class TechFactory
{
    public abstract Laptop CreateLaptop();
    public abstract Netbook CreateNetbook();
    public abstract EBook CreateEBook();
    public abstract Smartphone CreateSmartphone();
}

// Конкретні фабрики для кожного бренду
public class IProneFactory : TechFactory
{
    public override Laptop CreateLaptop()
    {
        return new IProneLaptop();
    }

    public override Netbook CreateNetbook()
    {
        return new IProneNetbook();
    }

    public override EBook CreateEBook()
    {
        return new IProneEBook();
    }

    public override Smartphone CreateSmartphone()
    {
        return new IProneSmartphone();
    }
}

public class KiaomiFactory : TechFactory
{
    public override Laptop CreateLaptop()
    {
        return new KiaomiLaptop();
    }

    public override Netbook CreateNetbook()
    {
        return new KiaomiNetbook();
    }

    public override EBook CreateEBook()
    {
        return new KiaomiEBook();
    }

    public override Smartphone CreateSmartphone()
    {
        return new KiaomiSmartphone();
    }
}

public class BalaxyFactory : TechFactory
{
    public override Laptop CreateLaptop()
    {
        return new BalaxyLaptop();
    }

    public override Netbook CreateNetbook()
    {
        return new BalaxyNetbook();
    }

    public override EBook CreateEBook()
    {
        return new BalaxyEBook();
    }

    public override Smartphone CreateSmartphone()
    {
        return new BalaxySmartphone();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Приклад використання
        TechFactory factory1 = new IProneFactory();
        Laptop laptop1 = factory1.CreateLaptop();
        Netbook netbook1 = factory1.CreateNetbook();
        EBook ebook1 = factory1.CreateEBook();
        Smartphone smartphone1 = factory1.CreateSmartphone();

        Console.WriteLine("Devices from IProne factory:");
        laptop1.DisplayInfo();
        netbook1.DisplayInfo();
        ebook1.DisplayInfo();
        smartphone1.DisplayInfo();

        TechFactory factory2 = new KiaomiFactory();
        Laptop laptop2 = factory2.CreateLaptop();
        Netbook netbook2 = factory2.CreateNetbook();
        EBook ebook2 = factory2.CreateEBook();
        Smartphone smartphone2 = factory2.CreateSmartphone();

        Console.WriteLine("\nDevices from Kiaomi factory:");
        laptop2.DisplayInfo();
        netbook2.DisplayInfo();
        ebook2.DisplayInfo();
        smartphone2.DisplayInfo();

        TechFactory factory3 = new BalaxyFactory();
        Laptop laptop3 = factory3.CreateLaptop();
        Netbook netbook3 = factory3.CreateNetbook();
        EBook ebook3 = factory3.CreateEBook();
        Smartphone smartphone3 = factory3.CreateSmartphone();

        Console.WriteLine("\nDevices from Balaxy factory:");
        laptop3.DisplayInfo();
        netbook3.DisplayInfo();
        ebook3.DisplayInfo();
        smartphone3.DisplayInfo();
    }
}
