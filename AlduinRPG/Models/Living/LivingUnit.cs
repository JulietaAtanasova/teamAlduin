namespace AlduinRPG.Models
{
    using System;
    using Interfaces;

    public abstract class LivingUnit : Unit, ILiving, IMovable, IResurrectable
    {
        private int maxHealth;
        private int currentHealth;
        private int attackStrength;
        private int level;

        protected LivingUnit(
            Coordinates coordinates,
            int maxHealth,
            int attackStrength,
            int level)
            : base(coordinates)
        {
            this.MaxHealth = maxHealth;
            this.CurrentHealth = this.MaxHealth;
            this.AttackStrength = attackStrength;
            this.Level = level;
        }

        public bool IsAlive
        {
            get
            {
                return this.CurrentHealth > 0;
            }
        }

        public int MaxHealth
        {
            get { return this.maxHealth; }
            protected set { this.maxHealth = value; }
        }

        public int CurrentHealth
        {
            get
            {
                return this.currentHealth;
            }

            protected set
            {
                if (value < 0)
                {
                    value = 0;
                }

                if (value > this.MaxHealth)
                {
                    value = this.MaxHealth;
                }

                this.currentHealth = value;
            }
        }

        public int AttackStrength
        {
            get { return this.attackStrength; }

            protected set { this.attackStrength = value; }
        }

        public int Level
        {
            get { return this.level; }

            protected set { this.level = value; }
        }

        public int PhysicalAttack()
        {
            return this.AttackStrength;
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    this.Coordinates = new Coordinates(this.Coordinates.X, this.Coordinates.Y - 1);
                    break;
                case Direction.Right:
                    this.Coordinates = new Coordinates(this.Coordinates.X + 1, this.Coordinates.Y);
                    break;
                case Direction.Left:
                    this.Coordinates = new Coordinates(this.Coordinates.X - 1, this.Coordinates.Y);
                    break;
                case Direction.Down:
                    this.Coordinates = new Coordinates(this.Coordinates.X, this.Coordinates.Y + 1);
                    break;
                default:
                    throw new ArgumentException("Invalid direction", "direction");
            }
        }

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
