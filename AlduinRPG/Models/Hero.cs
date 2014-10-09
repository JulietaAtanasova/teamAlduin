namespace AlduinRPG.Models
{
    public abstract class Hero : LivingUnit
    {
        public const int MaxLives = 3; // ?????

        public Hero(Coordinates coordinates, int maxHealth, int attackStrength, int level, 
            int maxMana, int experience, int lives, int recoverySpeedHealth, int recoverySpeedMana)
            : base(coordinates, maxHealth, attackStrength, level)
        {
            this.CurrentMana = maxMana;
            this.MaxMana = maxMana;
            this.CurrentHealth = maxHealth;
            this.Experience = experience;
            this.Lives = lives;
            this.RecoverySpeedHealth = recoverySpeedHealth;
            this.RecoverySpeedMana = recoverySpeedMana;
        }

        public int MaxMana { get; set; }

        public int CurrentMana { get; set; }

        public int Experience { get; set; }

        public int Lives { get; set; }

        public int RecoverySpeedHealth { get; set; }

        public int RecoverySpeedMana { get; set; }

        public void TakeChest()
        {
            // TODO
        }

        public void RecoverMana()
        {
            // TODO
        }

        public void RecoverHealth()
        {
            // TODO
        }

        public void CastMagic()
        {
            // TODO
        }
    }
}
