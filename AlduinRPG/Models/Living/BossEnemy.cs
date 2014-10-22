namespace AlduinRPG.Models
{
    public class BossEnemy : Enemy
    {
        private const EnemyType DefaultType = EnemyType.BossEnemy;
        private const int DefaultMaxHealth = 500;
        private const int DefaultAttackStrength = 20;
        private const int DefaultLevel = 1;
        public BossEnemy(Coordinates coordinates)
            : base(BossEnemy.DefaultType, coordinates, BossEnemy.DefaultMaxHealth, BossEnemy.DefaultAttackStrength, BossEnemy.DefaultLevel)
        {
        }
    }
}
