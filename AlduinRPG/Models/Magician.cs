namespace AlduinRPG.Models
{
    class Magician : Hero
    {
        private const int DefaultMaxHealth = 200; 
        private const int DefaultAttackStrength = 30;
        private const int DefaultLevel = 1;
        private const int DefaultMaxMana = 120;
        private const int DefaultExperience = 1;
        private const int DefaultLives = 1;
        private const int DefaultRecoverySpeedHealth = 2;
        private const int DefaultrecoverySpeedMana = 4;
        public Magician(Coordinates coordinates)
            : base(coordinates, DefaultMaxHealth, DefaultAttackStrength, DefaultLevel, DefaultMaxMana, DefaultExperience, DefaultLives, DefaultRecoverySpeedHealth, DefaultrecoverySpeedMana)
        {
        }
    }
}
