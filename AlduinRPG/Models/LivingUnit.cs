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

        public abstract void Move(Direction direction);

        public virtual void Resurrect(Coordinates coordinates)
        {
            this.Coordinates = coordinates;
            this.CurrentHealth = this.MaxHealth;
        }

        public void TakeDamage(int attack)
        {
            this.CurrentHealth -= attack;
        }

    }
}
