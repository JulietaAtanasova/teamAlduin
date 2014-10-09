namespace AlduinRPG.Models
{
    public abstract class LivingUnit : Unit, IAttackable, IMovable, IResurrectable
    {
        protected LivingUnit(Coordinates coordinates, int maxHealth, int attackStrength, int level)
            : base(coordinates)
        {
            this.MaxHealth = maxHealth;
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

        public void Ressurect()
        {
            // TODO live--, coords?, maxHealth, maxMana
        }
    }
}
