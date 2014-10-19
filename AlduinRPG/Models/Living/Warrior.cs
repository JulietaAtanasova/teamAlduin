namespace AlduinRPG.Models
{
    public class Warrior : Hero
    {
        private const int DefaultMaxHealth = 300;
        private const int DefaultAttackStrength = 80;
        private const int DefaultLevel = 1;
        private const int DefaultMaxMana = 70;
        private const int DefaultExperience = 1;
        private const int DefaultLives = 1;
        private const int DefaultRecoverySpeedHealth = 3;
        private const int DefaultrecoverySpeedMana = 1;
        public Warrior(Coordinates coordinates)
            : base(coordinates, Warrior.DefaultMaxHealth, Warrior.DefaultAttackStrength,
            Warrior.DefaultLevel, Warrior.DefaultMaxMana, Warrior.DefaultExperience, Warrior.DefaultLives,
            Warrior.DefaultRecoverySpeedHealth, Warrior.DefaultrecoverySpeedMana)
        {
        }
    }
}
