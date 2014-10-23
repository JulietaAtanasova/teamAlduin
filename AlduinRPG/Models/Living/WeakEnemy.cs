namespace AlduinRPG.Models
{
    public class WeakEnemy : Enemy
    {
        private const EnemyType DefaultType = EnemyType.WeakEnemy;
        private const int DefaultMaxHealth = 120;
        private const int DefaultAttackStrength = 50;
        private const int DefaultLevel = 1;

        public WeakEnemy(Coordinates coordinates)
            : base(
            WeakEnemy.DefaultType, 
            coordinates, 
            WeakEnemy.DefaultMaxHealth, 
            WeakEnemy.DefaultAttackStrength, 
            WeakEnemy.DefaultLevel)
        {
            this.EnemyType = EnemyType.WeakEnemy;
        }
    }
}
