namespace AlduinRPG.Models
{
    public class WeakEnemy : Enemy
    {
        // TODO constants health, attack, etc.
        public WeakEnemy(Coordinates coordinates, int maxHealth, int attackStrength, int level)
            : base(coordinates, maxHealth, attackStrength, level)
        {
        }
    }
}
