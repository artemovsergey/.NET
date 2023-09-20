using System;
using System.Collections.Generic;
using System.Text;

namespace D_OOP
{
    public class Point2D
    {
        private int x;
        private int y;

        public Point2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Character
    {
        private readonly int speed = 10;

        public int Health { get; set; } = 100;

        public Race Race { get; private set; }

        public int Armor { get; private set; }

        public string Name { get; private set; }

        public Character(Race race)
        {
            Race = race;

            switch (race)
            {
                case Race.Elf:
                    Armor = 30;
                    break;
                case Race.Ork:
                    Armor = 40;
                    break;
                case Race.Terrain:
                    Armor = 20;
                    break;
                default:
                    throw new ArgumentException("Unknown race.");                    
            }

            if(race == Race.Terrain)
            {
                Armor = 20;
            }
            else if (race == Race.Ork)
            {
                Armor = 40;
            }
            else if(race == Race.Elf)
            {
                Armor = 30;
            }
            else
            {
                throw new ArgumentException("Unknown race.");
            }
        }

        public Character(string name, int armor)
        {
            if (name == null)
                throw new ArgumentNullException("name arg can't be null");

            if (armor < 0 || armor > 100)
                throw new ArgumentException("armor can't be less than 0 or greater than 100");

            Name = name;
            Armor = armor;
        }

        public Character(Race race, int armor)
        {
            Race = race;
            Armor = armor;
        }

        //public Character(string race, int armor, int speed)
        //{
        //    Race = race;
        //    Armor = armor;
        //    this.speed = speed;
        //}

        public void Hit(int damage)
        {
            if (Health == 0)
                throw new InvalidOperationException("Can't hit a dead character.");

            if (damage > Health)
                throw new ArgumentException("damage can't be greater than current Health");

            if (damage > Health)
                damage = Health;

            //health -= damage;
            Health -= damage;            
        }

        public int PrintSpeed()
        {
            return speed;
        }

        public void IncreaseSpeed()
        {
            //speed += 10;
        }
    }
}
