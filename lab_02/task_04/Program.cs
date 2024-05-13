using System;
using System.Collections.Generic;

// Клас вірусу
public class Virus : ICloneable
{
    public double Weight { get; set; }
    public int Age { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public List<Virus> Children { get; set; }

    public Virus(double weight, int age, string name, string type)
    {
        Weight = weight;
        Age = age;
        Name = name;
        Type = type;
        Children = new List<Virus>();
    }

    // Метод для додавання дитини
    public void AddChild(Virus child)
    {
        Children.Add(child);
    }

    // Метод клонування вірусу
    public object Clone()
    {
        Virus clone = new Virus(Weight, Age, Name, Type);
        foreach (var child in Children)
        {
            clone.AddChild((Virus)child.Clone());
        }
        return clone;
    }

    // Метод для виводу інформації про вірус
    public void DisplayInfo()
    {
        Console.WriteLine($"Name: {Name}, Type: {Type}, Age: {Age}, Weight: {Weight}");
        foreach (var child in Children)
        {
            Console.Write("  ");
            child.DisplayInfo();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Створюємо дерево вірусів
        Virus parentVirus = new Virus(1.5, 2, "Parent", "Flu");
        Virus childVirus1 = new Virus(1.2, 1, "Child1", "Flu");
        Virus childVirus2 = new Virus(1.1, 1, "Child2", "Flu");
        Virus grandChildVirus = new Virus(1.0, 0, "GrandChild", "Flu");

        parentVirus.AddChild(childVirus1);
        parentVirus.AddChild(childVirus2);
        childVirus1.AddChild(grandChildVirus);

        // Клонуємо вірус та виводимо інформацію про клон
        Virus clonedVirus = (Virus)parentVirus.Clone();

        Console.WriteLine("Original virus:");
        parentVirus.DisplayInfo();

        Console.WriteLine("\nCloned virus:");
        clonedVirus.DisplayInfo();
    }
}
