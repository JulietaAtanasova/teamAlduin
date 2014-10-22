namespace AlduinRPG.Models
{
    public abstract class Enemy : LivingUnit
    {
        private EnemyType enemyType;

        protected Enemy(EnemyType enemyType, Coordinates coordinates, int maxHealth, int attackStrength, int level)
            : base(coordinates, maxHealth, attackStrength, level)
        {
            this.EnemyType = enemyType;
        }

        public EnemyType EnemyType
        {
            get { return this.enemyType; }

            set { this.enemyType = value; }
        }
    }
}
