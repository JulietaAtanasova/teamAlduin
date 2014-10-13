﻿using System;
using System.Security.Cryptography.X509Certificates;
using AlduinRPG.Interfaces;

namespace AlduinRPG.Models
{
    public abstract class LivingUnit : Unit, ILiving, IMovable
    {
        protected LivingUnit(Coordinates coordinates, int maxHealth, int attackStrength, int level)
            : base(coordinates)
        {
            this.CurrentHealth = maxHealth;
            this.AttackStrength = attackStrength;
            this.Level = level;
        }

        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int AttackStrength { get; set; }
        public int Level { get; set; }
        public Direction Direction { get; set; }
        public void PhysicallAttack()
        {
            // TODO
        }

        public void Move()
        {
            // TODO
        }

        public Coordinates Resurrect(GameMap gameMap)
        {
            Random random = new Random();
            int x = random.Next(gameMap.Width + 1, gameMap.Width);
            int y = random.Next(gameMap.Height + 1, gameMap.Height);
            Coordinates resurrectCoordinates = new Coordinates(x, y);
            return resurrectCoordinates;
        }

        public void TakeDamage(int attack)
        {
            this.CurrentHealth -= attack;
        }

    }
}
