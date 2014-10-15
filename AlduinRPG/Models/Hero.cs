namespace AlduinRPG.Models
{
    public abstract class Hero : LivingUnit
    {
        private const int MaxLives = 5;
        private const int MaxLevelExpirience = 100;

        public Hero(Coordinates coordinates, int maxHealth, int attackStrength, int level,
            int maxMana, int currentExperience, int lives, int recoverySpeedHealth, int recoverySpeedMana)
            : base(coordinates, maxHealth, attackStrength, level)
        {
            this.CurrentMana = maxMana;
            this.MaxMana = maxMana;
            this.CurrentExperience = currentExperience;
            this.Lives = lives;
            this.RecoverySpeedHealth = recoverySpeedHealth;
            this.RecoverySpeedMana = recoverySpeedMana;
        }

        public int MaxMana { get; set; }
        public int CurrentMana { get; set; }
        public int RecoverySpeedMana { get; set; }
        public int ExperienceIncreasment { get; set; }
        public int CurrentExperience { get; set; }
        public int Lives { get; set; }
        public int RecoverySpeedHealth { get; set; }


        private void TakeChest(Chest chest)
        {
            switch (chest.ChestType)
            {
                case ChestType.Life:
                    this.Lives++;
                    if (this.Lives > MaxLives)
                    {
                        this.Lives = MaxLives;
                    }
                    break;
                case ChestType.MaxHealth:
                    this.CurrentHealth = this.MaxHealth;
                    break;
                case ChestType.MaxMana:
                    this.CurrentMana = MaxMana;
                    break;
            }
        }

        public void RecoverMana()
        {
            this.CurrentMana += this.RecoverySpeedMana;
            if (this.CurrentMana > this.MaxMana)
            {
                this.CurrentMana = this.MaxMana;
            }
        }

        public void RecoverHealth()
        {
            this.CurrentHealth += this.RecoverySpeedHealth;
            if (this.CurrentHealth > this.MaxHealth)
            {
                this.CurrentHealth = this.MaxHealth;
            }
        }

        public int CastMagic()
        {
            return this.CurrentMana;
        }

        public void GainExperience()
        {
            this.CurrentExperience += this.ExperienceIncreasment;
        }
    }
}
