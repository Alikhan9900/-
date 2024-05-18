using System;
using System.Collections.Generic;

namespace BuilderPattern
{
    class Character
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Height { get; set; }
        public string Build { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string Clothing { get; set; }
        public List<string> Inventory { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Type: {Type}, Height: {Height}, Build: {Build}, Hair Color: {HairColor}, Eye Color: {EyeColor}, Clothing: {Clothing}";
        }
    }

    interface ICharacterBuilder
    {
        ICharacterBuilder SetName(string name);
        ICharacterBuilder SetType(string type);
        ICharacterBuilder SetHeight(string height);
        ICharacterBuilder SetBuild(string build);
        ICharacterBuilder SetHairColor(string hairColor);
        ICharacterBuilder SetEyeColor(string eyeColor);
        ICharacterBuilder SetClothing(string clothing);
        ICharacterBuilder AddToInventory(List<string> inventory);
        Character Build();
    }

    class HeroBuilder : ICharacterBuilder
    {
        private Character character = new Character();

        public ICharacterBuilder SetName(string name)
        {
            character.Name = name;
            return this;
        }

        public ICharacterBuilder SetType(string type)
        {
            character.Type = type;
            return this;
        }

        public ICharacterBuilder SetHeight(string height)
        {
            character.Height = height;
            return this;
        }

        public ICharacterBuilder SetBuild(string build)
        {
            character.Build = build;
            return this;
        }

        public ICharacterBuilder SetHairColor(string hairColor)
        {
            character.HairColor = hairColor;
            return this;
        }

        public ICharacterBuilder SetEyeColor(string eyeColor)
        {
            character.EyeColor = eyeColor;
            return this;
        }

        public ICharacterBuilder SetClothing(string clothing)
        {
            character.Clothing = clothing;
            return this;
        }

        public ICharacterBuilder AddToInventory(List<string> inventory)
        {
            if (inventory != null)
            {
                if (character.Inventory == null)
                    character.Inventory = new List<string>();

                character.Inventory.AddRange(inventory);
            }
            return this;
        }

        public Character Build()
        {
            return character;
        }
    }

    class CharacterDirector
    {
        private ICharacterBuilder characterBuilder;

        public CharacterDirector(ICharacterBuilder builder)
        {
            characterBuilder = builder;
        }

        public Character ConstructCharacter(string name, string type, string height, string build, string hairColor, string eyeColor, string clothing, List<string> inventory = null)
        {
            return characterBuilder
                .SetName(name)
                .SetType(type)
                .SetHeight(height)
                .SetBuild(build)
                .SetHairColor(hairColor)
                .SetEyeColor(eyeColor)
                .SetClothing(clothing)
                .AddToInventory(inventory)
                .Build();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var heroBuilder = new HeroBuilder();
            var characterDirector = new CharacterDirector(heroBuilder);
            var hero = characterDirector.ConstructCharacter("HeroName", "Good", "6 feet", "Athletic", "Blonde", "Blue", "Armor", new List<string> { "Sword", "Shield" });
            Console.WriteLine("Hero:");
            Console.WriteLine(hero);

            var enemyBuilder = new HeroBuilder();
            characterDirector = new CharacterDirector(enemyBuilder);
            var enemy = characterDirector.ConstructCharacter("EnemyName", "Evil", "7 feet", "Hulking", "Black", "Red", "Dark Robes", new List<string> { "Dark Magic", "Cursed Blade" });
            Console.WriteLine("\nEnemy:");
            Console.WriteLine(enemy);
        }
    }
}
