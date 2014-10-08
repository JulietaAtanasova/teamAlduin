namespace AlduinRPG.Models
{
    public abstract class Hero : LivingUnit
    {
        public const int MaxLives = 3; // ?????

        // TODO constructor;

        public int CastMagic()
        {
            return 0;
        }

        public Hero(Coordinates coordinates, int maxHealth, int health, int attackStrength, int level, Direction direction, int maxMana, int mana, int experience, int lives, int recoverySpeedHealth, int recoverySpeedMana)
            : base(coordinates, maxHealth, health, attackStrength, level, direction)
        {
            this.MaxMana = maxMana;
            this.Mana = mana;
            this.Experience = experience;
            this.Lives = lives;
            this.RecoverySpeedHealth = recoverySpeedHealth;
            this.RecoverySpeedMana = recoverySpeedMana;
        }

        public int MaxMana { get; set; }

        public int Mana { get; set; }

        public int Experience { get; set; }

        public int Lives { get; set; }

        public int RecoverySpeedHealth { get; set; }

        public int RecoverySpeedMana { get; set; }

        public void TakeChest()
        {

        }

        public void RecoverMana()
        {

        }

        public void RecoverHealth()
        {

        }
    }
}
