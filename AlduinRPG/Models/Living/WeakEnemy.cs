namespace AlduinRPG.Models
{
    public class WeakEnemy : Enemy
    {
        private const int DefaultMaxHealth = 120;
        private const int DefaultAttackStrength = 50;
        private const int DefaultLevel = 1;
        public WeakEnemy(Coordinates coordinates)
            : base(coordinates, DefaultMaxHealth, DefaultAttackStrength, DefaultLevel)
        {
            this.EnemyType = EnemyType.WeakEnemy;
        }
    }
}
