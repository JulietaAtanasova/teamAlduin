namespace AlduinRPG.Models
{
    public class BossEnemy : Enemy
    {
        private const int DefaultMaxHealth = 120;
        private const int DefaultAttackStrength = 50;
        private const int DefaultLevel = 1;
        public BossEnemy(Coordinates coordinates)
            : base(coordinates, DefaultMaxHealth, DefaultAttackStrength, DefaultLevel)
        {
        }
    }
}
