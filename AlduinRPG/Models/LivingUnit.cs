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

        public void Resurrect()
        {
            // TODO: Return Coordinates?????
        }

        public int TakeDamage(int attackPoints)
        {
            this.CurrentHealth -= attackPoints;
            return this.CurrentHealth;
        }

    }
}
