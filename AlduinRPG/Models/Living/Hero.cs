namespace AlduinRPG.Models
{
    using System.Collections.Generic;

    public abstract class Hero : LivingUnit
    {
        private const int MaxLives = 5;
        private const int MaxLevelExpirience = 100;
        private const int experienceIncreasment = 20;
        private int maxMana;
        private int currentMana;
        private int recoverySpeedMana;
        private int currentExperience;
        private int maxExperience;
        private int currentLives;
        private int recoverySpeedHealth;

        protected Hero(
            Coordinates coordinates,
            HeroType heroType,
            int maxHealth,
            int attackStrength,
            int level,
            int maxMana,
            int currentExperience,
            int maxExpirience,
            int currentLives,
            int recoverySpeedHealth,
            int recoverySpeedMana)
            : base(coordinates, maxHealth, attackStrength, level)
        {
            this.HeroType = heroType;
            this.CurrentMana = maxMana;
            this.MaxMana = maxMana;
            this.CurrentExperience = currentExperience;
            this.CurrentLives = currentLives;
            this.RecoverySpeedHealth = recoverySpeedHealth;
            this.RecoverySpeedMana = recoverySpeedMana;
            this.MaxExperience = maxExpirience;
        }

        public int MaxMana
        {
            get { return this.maxMana; }

            protected set { this.maxMana = value; }
        }

        public int CurrentMana
        {
            get { return this.currentMana; }

            protected set
            {
                if (value > this.MaxMana)
                {
                    value = this.MaxMana;
                }

                this.currentMana = value;
            }
        }

        public int RecoverySpeedMana
        {
            get { return this.recoverySpeedMana; }

            protected set { this.recoverySpeedMana = value; }
        }

        public int CurrentExperience
        {
            get { return this.currentExperience; }

            protected set
            {
                this.currentExperience = value;
            }
        }

        public int MaxExperience
        {
            get { return maxExperience; }

            set { this.maxExperience = value; }
        }

        public int CurrentLives
        {
            get { return this.currentLives; }

            protected set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this.currentLives = value;
            }
        }

        public int RecoverySpeedHealth
        {
            get { return this.recoverySpeedHealth; }

            protected set { this.recoverySpeedHealth = value; }
        }

        public HeroType HeroType
        {
            get;
            protected set;
        }

        public void TakeChest(Chest chest)
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
        }

        public void RecoverHealth()
        {
            this.CurrentHealth += this.RecoverySpeedHealth;
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

        public void GainExperience(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.WeakEnemy:
                    this.CurrentExperience += Hero.experienceIncreasment;
                    break;
                case EnemyType.BossEnemy:
                    this.CurrentExperience += Hero.experienceIncreasment * 2;
                    break;
            }
            
            if (this.currentExperience >= this.MaxExperience)
            {
                this.currentExperience = 0;
                this.Level ++;
            }
        }

        public override void Resurrect(Coordinates coordinates)
        {
            base.Resurrect(coordinates);
            this.CurrentMana = this.MaxMana;
            this.CurrentLives--;
        }


    }
}
