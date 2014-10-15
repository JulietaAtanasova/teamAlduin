namespace AlduinRPG.Models
{
    class Magician : Hero
    {
        // TODO constants
        public Magician(Coordinates coordinates, int maxHealth, int attackStrength, int level,
            int maxMana, int experience, int lives, int recoverySpeedHealth, int recoverySpeedMana)
            : base(coordinates, maxHealth, attackStrength, level, maxMana, experience, lives, recoverySpeedHealth, recoverySpeedMana)
        {
        }
    }
}
