namespace AlduinRPG.Models
{
    public class BossEnemy : Enemy
    {
        // TODO constants health, attack, etc.

        public BossEnemy(Coordinates coordinates, int maxHealth, int attackStrength, int level)
            : base(coordinates, maxHealth, attackStrength, level)
        {
        }
    }
}
