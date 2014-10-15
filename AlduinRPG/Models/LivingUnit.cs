namespace AlduinRPG.Models
{
    using System;
    using AlduinRPG.Interfaces;


    public abstract class LivingUnit : Unit, ILiving, IMovable
    {
        protected LivingUnit(Coordinates coordinates, int maxHealth, int attackStrength, int level, bool isAlive = true)
            : base(coordinates)
        {
            this.CurrentHealth = maxHealth;
            this.MaxHealth = maxHealth;
            this.AttackStrength = attackStrength;
            this.Level = level;
            this.IsAlive = isAlive;
        }

        public bool IsAlive { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int AttackStrength { get; set; }
        public int Level { get; set; }
        public Direction Direction { get; set; }
        public int PhysicallAttack()
        {
            return this.AttackStrength;
        }

        public void Move()
        {
            // TODO: 
        }

        public Coordinates Resurrect(GameMap gameMap)
        {
            Random random = new Random();
            int x = random.Next(1, gameMap.Width);
            int y = random.Next(1, gameMap.Height);
            Coordinates resurrectCoordinates = new Coordinates(x, y);
            return resurrectCoordinates;
        }

        public void TakeDamage(int attack)
        {
            this.CurrentHealth -= attack;
        }

    }
}
