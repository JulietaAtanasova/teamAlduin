namespace AlduinRPG.Models
{
    public class Warrior : Hero
    {
        // TODO constants health, attack, mana, lives etc.
        
        public Warrior(Coordinates coordinates, int maxHealth, int attackStrength, int level, 
            int maxMana, int experience, int lives, int recoverySpeedHealth, int recoverySpeedMana)
            : base(coordinates, maxHealth, attackStrength, level, maxMana, experience, lives, recoverySpeedHealth, recoverySpeedMana)
        {
        }
    }
}
