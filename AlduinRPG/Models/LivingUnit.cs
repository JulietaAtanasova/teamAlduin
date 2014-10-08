namespace AlduinRPG.Models
{
    public abstract class LivingUnit : Unit, IAttackable, IMovable, IResurrectable
    {
        protected LivingUnit(Coordinates coordinates, int maxHealth, int health, int attackStrength, int level, Direction direction)
            : base(coordinates)
        {
            this.MaxHealth = maxHealth;
            this.Health = health;
            this.AttackStrength = attackStrength;
            this.Level = level;
            this.Direction = direction;
        }

        public int MaxHealth { get; set; }
        public int Health { get; set; }
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

        public void Ressurect()
        {
            // TODO live--, coords?, maxHealth, maxMana
        }
    }
}
