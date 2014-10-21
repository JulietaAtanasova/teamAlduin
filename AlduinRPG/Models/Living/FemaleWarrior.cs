namespace AlduinRPG.Models
{
    public class FemaleWarrior : Hero
    {
        private const int DefaultMaxHealth = 250;
        private const int DefaultAttackStrength = 50;
        private const int DefaultLevel = 1;
        private const int DefaultMaxMana = 70;
        private const int DefaultExperience = 1;
        private const int DefaultLives = 1;
        private const int DefaultRecoverySpeedHealth = 5;
        private const int DefaultrecoverySpeedMana = 1;
        public FemaleWarrior(Coordinates coordinates)
            : base(coordinates, FemaleWarrior.DefaultMaxHealth, FemaleWarrior.DefaultAttackStrength,
            FemaleWarrior.DefaultLevel, FemaleWarrior.DefaultMaxMana, FemaleWarrior.DefaultExperience, FemaleWarrior.DefaultLives,
            FemaleWarrior.DefaultRecoverySpeedHealth, FemaleWarrior.DefaultrecoverySpeedMana)
        {
        }
    }
}
