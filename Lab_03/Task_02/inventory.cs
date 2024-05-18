abstract class InventoryDecorator : Hero
{
    protected Hero hero;

    public InventoryDecorator(Hero hero)
    {
        this.hero = hero;
    }

    public override string GetDescription() => hero.GetDescription();
    public override int GetPower() => hero.GetPower();
}

class Armor : InventoryDecorator
{
    public Armor(Hero hero) : base(hero) { }

    public override string GetDescription() => hero.GetDescription() + ", Armor";
    public override int GetPower() => hero.GetPower() + 3;
}

class Weapon : InventoryDecorator
{
    public Weapon(Hero hero) : base(hero) { }

    public override string GetDescription() => hero.GetDescription() + ", Weapon";
    public override int GetPower() => hero.GetPower() + 5;
}

class Artifact : InventoryDecorator
{
    public Artifact(Hero hero) : base(hero) { }

    public override string GetDescription() => hero.GetDescription() + ", Artifact";
    public override int GetPower() => hero.GetPower() + 2;
}
