using System;

abstract class Hero
{
    public abstract string GetDescription();
    public abstract int GetPower();
}

class Warrior : Hero
{
    public override string GetDescription() => "Warrior";
    public override int GetPower() => 10;
}

class Mage : Hero
{
    public override string GetDescription() => "Mage";
    public override int GetPower() => 8;
}

class Paladin : Hero
{
    public override string GetDescription() => "Paladin";
    public override int GetPower() => 9;
}

