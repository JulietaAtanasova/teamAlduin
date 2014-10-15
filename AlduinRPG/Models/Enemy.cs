namespace AlduinRPG.Models
{
    public abstract class Enemy : LivingUnit
    {
        protected Enemy(Coordinates coordinates, int maxHealth, int attackStrength, int level)
            : base(coordinates, maxHealth, attackStrength, level)
        {
        }

        public override void Move(Direction direction)
        {
            // TODO
        }
    }
}
