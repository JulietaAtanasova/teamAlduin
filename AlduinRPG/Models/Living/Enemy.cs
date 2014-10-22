namespace AlduinRPG.Models
{
    public abstract class Enemy : LivingUnit
    {
        protected Enemy(Coordinates coordinates, int maxHealth, int attackStrength, int level)
            : base(coordinates, maxHealth, attackStrength, level)
        {
        }

        public EnemyType EnemyType { get; set; }

        public override Coordinates Move(Direction direction)
        {
            // TODO : random logic???
            return new Coordinates(this.Coordinates.X, this.Coordinates.Y);
        }
    }
}
