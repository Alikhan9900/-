using System;
using System.Collections.Generic;

namespace DesignPatterns.Mediator
{
    // Інтерфейс Посередника
    interface ICommandCentre
    {
        void RegisterRunway(Runway runway);
        void RegisterAircraft(Aircraft aircraft);
        bool CanLand(Aircraft aircraft);
        void Land(Aircraft aircraft);
        void TakeOff(Aircraft aircraft);
    }

    // Реалізація Посередника
    class CommandCentre : ICommandCentre
    {
        private List<Runway> _runways = new List<Runway>();
        private List<Aircraft> _aircrafts = new List<Aircraft>();

        public void RegisterRunway(Runway runway)
        {
            _runways.Add(runway);
        }

        public void RegisterAircraft(Aircraft aircraft)
        {
            _aircrafts.Add(aircraft);
        }

        public bool CanLand(Aircraft aircraft)
        {
            foreach (var runway in _runways)
            {
                if (runway.IsFree())
                {
                    return true;
                }
            }
            return false;
        }

        public void Land(Aircraft aircraft)
        {
            foreach (var runway in _runways)
            {
                if (runway.IsFree())
                {
                    runway.Land(aircraft);
                    break;
                }
            }
        }

        public void TakeOff(Aircraft aircraft)
        {
            var runway = aircraft.CurrentRunway;
            if (runway != null)
            {
                runway.TakeOff(aircraft);
            }
        }
    }

    // Клас представлення злітно-посадкової смуги
    class Runway
    {
        public readonly Guid Id = Guid.NewGuid();
        private bool _isBusy;
        private Aircraft _aircraft;

        public bool IsFree()
        {
            return !_isBusy;
        }

        public void Land(Aircraft aircraft)
        {
            Console.WriteLine($"Aircraft {aircraft.Name} is landing on Runway {Id}");
            _isBusy = true;
            _aircraft = aircraft;
        }

        public void TakeOff(Aircraft aircraft)
        {
            Console.WriteLine($"Aircraft {aircraft.Name} is taking off from Runway {Id}");
            _isBusy = false;
            _aircraft = null;
        }
    }

    // Клас представлення літака
    class Aircraft
    {
        public string Name { get; }
        public Runway CurrentRunway { get; set; }

        public Aircraft(string name)
        {
            Name = name;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створення посередника (командного центру)
            var commandCentre = new CommandCentre();

            // Створення злітно-посадкових смуг
            var runway1 = new Runway();
            var runway2 = new Runway();

            // Реєстрація злітно-посадкових смуг в командному центрі
            commandCentre.RegisterRunway(runway1);
            commandCentre.RegisterRunway(runway2);

            // Створення літаків
            var aircraft1 = new Aircraft("Boeing 747");
            var aircraft2 = new Aircraft("Airbus A380");

            // Реєстрація літаків в командному центрі
            commandCentre.RegisterAircraft(aircraft1);
            commandCentre.RegisterAircraft(aircraft2);

            // Перевірка можливості посадки
            if (commandCentre.CanLand(aircraft1))
            {
                commandCentre.Land(aircraft1);
            }
            else
            {
                Console.WriteLine($"Cannot land {aircraft1.Name}");
            }

            // Перевірка можливості посадки
            if (commandCentre.CanLand(aircraft2))
            {
                commandCentre.Land(aircraft2);
            }
            else
            {
                Console.WriteLine($"Cannot land {aircraft2.Name}");
            }

            // Виконання зльоту літаків
            commandCentre.TakeOff(aircraft1);
            commandCentre.TakeOff(aircraft2);
        }
    }
}
