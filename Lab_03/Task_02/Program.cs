using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using System;

class Program
{
    static void Main(string[] args)
    {
        Hero warrior = new Warrior();
        Console.WriteLine($"{warrior.GetDescription()} has power {warrior.GetPower()}");

        warrior = new Armor(warrior);
        Console.WriteLine($"{warrior.GetDescription()} has power {warrior.GetPower()}");

        warrior = new Weapon(warrior);
        Console.WriteLine($"{warrior.GetDescription()} has power {warrior.GetPower()}");

        warrior = new Artifact(warrior);
        Console.WriteLine($"{warrior.GetDescription()} has power {warrior.GetPower()}");

        Hero mage = new Mage();
        Console.WriteLine($"{mage.GetDescription()} has power {mage.GetPower()}");

        mage = new Armor(mage);
        mage = new Weapon(mage);
        mage = new Artifact(mage);
        Console.WriteLine($"{mage.GetDescription()} has power {mage.GetPower()}");

        Hero paladin = new Paladin();
        paladin = new Armor(paladin);
        paladin = new Weapon(paladin);
        paladin = new Weapon(paladin);  // Paladin with two weapons
        paladin = new Artifact(paladin);
        Console.WriteLine($"{paladin.GetDescription()} has power {paladin.GetPower()}");
    }
}
