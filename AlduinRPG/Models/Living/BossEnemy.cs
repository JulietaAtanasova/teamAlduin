namespace AlduinRPG.Models
{
    public class BossEnemy : Enemy
    {
        private const int DefaultMaxHealth = 500;
        private const int DefaultAttackStrength = 20;
        private const int DefaultLevel = 1;
        public BossEnemy(Coordinates coordinates)
            : base(coordinates, BossEnemy.DefaultMaxHealth, BossEnemy.DefaultAttackStrength, BossEnemy.DefaultLevel)
        {
            this.EnemyType = EnemyType.BossEnemy;
        }
    }
}
