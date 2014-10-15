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

        private void TakeChest(Chest chest)
        {
            switch (chest.ChestType)
            {
                case ChestType.Live:
                    // TODO: if Lives == MaxLives
                    this.Lives++;
                    break;
                case ChestType.MaxHealth:
                    // TODO: if CurrHealth == MaxHealth
                    this.CurrentHealth = this.MaxHealth;
                    break;
                case ChestType.MaxMana:
                    // TODO: if CurrMana == MaxMana
                    this.CurrentMana = MaxMana;
                    break;
            }
        }

        public void RecoverMana()
        {
            this.CurrentMana += this.RecoverySpeedMana;
        }

        public void RecoverHealth()
        {
            this.CurrentHealth += this.RecoverySpeedHealth;
        }

        public int CastMagic()
        {
            return this.CurrentMana;
        }

        public void GainExperience(int expirienceIncreasment)
        {
            this.Experience += expirienceIncreasment;
        }
    }
}
