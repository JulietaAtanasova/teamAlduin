namespace AlduinRPG.Models
{
    public class Magician : Hero
    {
        private const HeroType DefaultHeroType = HeroType.Magician;
        private const int DefaultMaxHealth = 200; 
        private const int DefaultAttackStrength = 30;
        private const int DefaultLevel = 1;
        private const int DefaultMaxMana = 120;
        private const int DefaultExperience = 1;
        private const int DefaultMaxExperience = 100;
        private const int DefaultLives = 1;
        private const int DefaultRecoverySpeedHealth = 2;
        private const int DefaultRecoverySpeedMana = 4;

        public Magician(Coordinates coordinates)
            : base(
            coordinates,
            Magician.DefaultHeroType, 
            Magician.DefaultMaxHealth,
            Magician.DefaultAttackStrength,
            Magician.DefaultLevel,
            Magician.DefaultMaxMana, 
            Magician.DefaultExperience,
            Magician.DefaultMaxExperience, 
            Magician.DefaultLives,
            Magician.DefaultRecoverySpeedHealth, 
            Magician.DefaultRecoverySpeedMana)
        {
        }
    }
}
