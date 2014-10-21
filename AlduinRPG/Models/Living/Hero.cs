using System.Collections.Generic;

namespace AlduinRPG.Models
{
    public abstract class Hero : LivingUnit
    {
        private const int MaxLives = 5;
        private const int MaxLevelExpirience = 100;
        private int maxMana;
        private int currentMana;
        private int recoverySpeedMana;
        private int experienceIncreasment;
        private int currentExperience;
        private int currentLives;
        private int recoverySpeedHealth;

        public Hero(Coordinates coordinates, int maxHealth, int attackStrength, int level,
            int maxMana, int currentExperience, int currentLives, int recoverySpeedHealth, int recoverySpeedMana)
            : base(coordinates, maxHealth, attackStrength, level)
        {
            this.CurrentMana = maxMana;
            this.MaxMana = maxMana;
            this.CurrentExperience = currentExperience;
            this.CurrentLives = currentLives;
            this.RecoverySpeedHealth = recoverySpeedHealth;
            this.RecoverySpeedMana = recoverySpeedMana;
        }

        public int MaxMana { get; set; }
        public int CurrentMana { get; set; }
        public int RecoverySpeedMana { get; set; }
        public int ExperienceIncreasment { get; set; }
        public int CurrentExperience { get; set; }
        public int CurrentLives { get; set; }
        public int RecoverySpeedHealth { get; set; }


        private void TakeChest(Chest chest)
        {
            switch (chest.ChestType)
            {
                case ChestType.Life:
                    this.CurrentLives++;
                    if (this.CurrentLives > MaxLives)
                    {
                        this.CurrentLives = MaxLives;
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

        public void Recover()
        {
            this.RecoverHealth();
            this.RecoverMana();
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

        public List<Magic> CastMagic()
        {
            int magicRange = this.GetMagicRange();
            List<Magic> magicFields = new List<Magic>();

            for (int x = this.Coordinates.X - magicRange; x <= this.Coordinates.X + magicRange; x++)
            {
                for (int y = this.Coordinates.Y - magicRange; y < this.Coordinates.Y + magicRange; y++)
                {
                    if (x == this.Coordinates.X && y == this.Coordinates.Y)
                    {
                        continue;
                    }

                    magicFields.Add(new Magic(new Coordinates(x, y), this.AttackStrength));
                }
            }

            return magicFields;
        }

        private int GetMagicRange()
        {
            int magicRange;
            if (this.CurrentMana >= this.MaxMana * 0.6)
            {
                magicRange = this.Level + 1;
                this.CurrentMana -= (int)(this.MaxMana * 0.6);
            }
            else if (this.CurrentMana >= this.MaxMana * 0.3)
            {
                magicRange = this.Level;
                this.CurrentMana -= (int)(this.MaxMana * 0.3);
            }
            else
            {
                magicRange = -1; // not to cast any magics
            }

            return magicRange;
        }

        public void GainExperience()
        {
            this.CurrentExperience += this.ExperienceIncreasment;
        }

        public override void Resurrect(Coordinates coordinates)
        {
            base.Resurrect(coordinates);
            this.CurrentMana = this.MaxMana;
            this.CurrentLives--;
        }

        public override Coordinates Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Coordinates(this.Coordinates.X, this.Coordinates.Y - 1);
                case Direction.Right:
                    return new Coordinates(this.Coordinates.X + 1, this.Coordinates.Y);
                case Direction.Left:
                    return new Coordinates(this.Coordinates.X - 1, this.Coordinates.Y);
                case Direction.Down:
                    return new Coordinates(this.Coordinates.X, this.Coordinates.Y + 1);
                default:
                    return new Coordinates(this.Coordinates.X, this.Coordinates.Y);
            }
        }
    }
}
