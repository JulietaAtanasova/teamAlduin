namespace AlduinRPG.Views
{
    using Models;
    using System.Drawing;
    using System;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.Linq;

    public class RendererView
    {
        private GameForm gameForm;
        private List<Unit> unitsToRender;
        public static List<PictureBox> pictureBoxes;
        public static List<ProgressBar> progressBars;
        private const string BossPath = "../../Resources/boss70x70.png";
        private const string BushPath = "../../Resources/bush.png";
        private const string ChestPath = "../../Resources/chest70x70.png";
        private const string ChooseFemaleWarriorPath = "../../Resources/chooseFemaleWarrior.png";
        private const string ChooseMagicianPath = "../../Resources/chooseMagician.png";
        private const string ChooseWarriorPath = "../../Resources/chooseWarrior.png";
        private const string EnemyPath = "../../Resources/enemy70x70.png";
        private const string FemaleWarriorPath = "../../Resources/female-warrior70x70.png";
        private const string GameOverPath = "../../Resources/gameover.png";
        private const string LogoPath = "../../Resources/logo.png";
        private const string MagicianPath = "../../Resources/magician70x70.png";
        private const string RockPath = "../../Resources/rock70x70.png";
        private const string TeleportPath = "../../Resources/teleport.png";
        private const string TreePath = "../../Resources/tree.png";
        private const string WarriorPath = "../../Resources/warrior70x70.png";
        private const string BackgroundPath = "../../Resources/grass70x70.png";
        private Color Yellow = Color.FromArgb(255, 231, 182, 54);
        private Color Gray = Color.Gray;
        private Font FontFamily = new Font("Sans-serif", 12, FontStyle.Bold);
        private Font HeadingFont = new Font("Sans-serif", 16, FontStyle.Bold); 
        private Size ButtonSize = new Size(150, 30);

        private Image bossImage;
        private Image bushImage;
        private Image chestImage;
        private Image chooseFemaleWarrior;
        private Image chooseWarrior;
        private Image chooseMagician;
        private Image enemyImage;
        private Image femaleWarriorImage;
        private Image gameOver;
        private Image logo;
        private Image magicianImage;
        private Image rockImage;
        private Image teleportImage;
        private Image treeImage;
        private Image warriorImage;
        private Image backgroundImage;

        private readonly Coordinates ProgressBarOffset = new Coordinates(15, 30);
        private readonly Coordinates UnitOffset = new Coordinates(70, 70);
        private const int TopBarOffset = 90;

        private List<Button> menuButtons = new List<Button>();
        private List<Button> heroesButtons = new List<Button>(); 

        public RendererView(GameForm gameForm)
        {
            this.gameForm = gameForm;
            LoadImages();
            RendererView.pictureBoxes = new List<PictureBox>();
            RendererView.progressBars = new List<ProgressBar>();
            this.unitsToRender = new List<Unit>();
            RenderTopBackground();
        }

        private void RenderTopBackground()
        {
            var picBox = new PictureBox();
            picBox.Size = new Size(630, 90);
            picBox.Location = new Point(0, 0);
            picBox.BackColor = Gray;
            picBox.ForeColor = Gray;
            this.gameForm.Controls.Add(picBox);
            this.gameForm.Controls.SetChildIndex(picBox, 999);
        }

        public List<Button> MenuButtons
        {
            get
            {
                return this.menuButtons;
            }
        }
        public List<Button> HeroesButtons
        {
            get
            {
                return this.heroesButtons;
            }
        }

        public void Render(Units units, GameMap gameMap)
        {
            var unitsToRender = getUnitsToRender(units);
            ClearScreen(units.Hero);
            RenderUnits(unitsToRender);
            RenderFrame(units.Hero);
        }

        private Units getUnitsToRender(Units units)
        {
            var unitsToReturn = new Units();

            unitsToReturn.Hero = units.Hero;
            unitsToReturn.Enemies = getEnemiesToRender(units.Enemies, units.Hero);
            unitsToReturn.Teleports = getTeleportsToRender(units.Teleports, units.Hero);
            unitsToReturn.Obstacles = getObstaclesToRender(units.Obstacles, units.Hero);
            unitsToReturn.Chests = getChestsToRender(units.Chests, units.Hero);

            return unitsToReturn;
        }

        private IDictionary<Coordinates, Chest> getChestsToRender(IDictionary<Coordinates, Chest> chests, Hero hero)
        {
            var returnDictionary = new Dictionary<Coordinates, Chest>();
            foreach (var chest in chests)
            {
                if (IsInRange(chest.Key, hero.Coordinates))
                {
                    returnDictionary.Add(chest.Key, chest.Value);
                }
            }
            return returnDictionary;
        }

        private IDictionary<Coordinates, Obstacle> getObstaclesToRender(IDictionary<Coordinates, Obstacle> obstacles, Hero hero)
        {
            var returnDictionary = new Dictionary<Coordinates, Obstacle>();
            foreach (var obstacle in obstacles)
            {
                if (IsInRange(obstacle.Key, hero.Coordinates))
                {
                    returnDictionary.Add(obstacle.Key, obstacle.Value);
                }
                else
                {
                    RemoveObject(obstacle.Value);
                }
            }
            return returnDictionary;
        }

        private IDictionary<Coordinates, Teleportation> getTeleportsToRender(IDictionary<Coordinates, Teleportation> teleports, Hero hero)
        {
            var returnDictionary = new Dictionary<Coordinates, Teleportation>();
            foreach (var teleport in teleports)
            {
                if (IsInRange(teleport.Key, hero.Coordinates))
                {
                    returnDictionary.Add(teleport.Key, teleport.Value);
                }
            }
            return returnDictionary;
        }

        private IDictionary<Coordinates, Enemy> getEnemiesToRender(IDictionary<Coordinates, Enemy> enemies, Hero hero)
        {
            var returnDictionary = new Dictionary<Coordinates, Enemy>();
            foreach (var enemy in enemies)
            {
                if (IsInRange(enemy.Key, hero.Coordinates))
                {
                    returnDictionary.Add(enemy.Key, enemy.Value);
                }
            }
            return returnDictionary;
        }

        private bool IsInRange(Coordinates objectCoordinates, Coordinates heroCoordinates)
        {
            bool collisionByX = objectCoordinates.X >= heroCoordinates.X - 4 &&
                                objectCoordinates.X <= heroCoordinates.X + 4;
            bool collisionByY = objectCoordinates.Y >= heroCoordinates.Y - 3 &&
                                objectCoordinates.Y <= heroCoordinates.Y + 3;
            if (collisionByX && collisionByY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ClearScreen(Hero hero)
        {
            List<Unit> unitsToRemove = new List<Unit>();
            foreach (var unit in this.unitsToRender)
            {
                if (!toRender(hero.Coordinates, unit.Coordinates))
                {
                    try
                    {
                        unitsToRemove.Add(unit);
                        var picBox = GetPictureBoxByObject(unit);
                        RendererView.pictureBoxes.Remove(picBox);
                        this.gameForm.Controls.Remove(picBox);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Whoops! Cant do that.. sorry. Error: " + e);
                    }
                }
                else
                {
                    ChangeObjectPosition(unit);
                }
            }
            //unitsToRender.RemoveAll(unitsToRemove.Contains);
            unitsToRemove.ForEach(RemoveObject);
        }

        private void ChangeObjectPosition(Unit unit)
        {
            var picBox = GetPictureBoxByObject(unit);
            var newLocation = new Point(unit.Coordinates.X * 70, unit.Coordinates.Y * 70);
            picBox.Location = newLocation;
            RendererView.pictureBoxes.Find(p => p.Tag == unit).Location = newLocation;
        }

        private Coordinates CalculateRelativeCoordinates(Coordinates mainCoordinates,
            Coordinates relativeObjectCoordinates)
        {
            Coordinates relativeCoordinates = new Coordinates();
            int differenceByX = mainCoordinates.X - relativeObjectCoordinates.X;
            int differenceByY = mainCoordinates.Y - relativeObjectCoordinates.Y;

            if (differenceByX < 0)
            {
                relativeCoordinates.X = mainCoordinates.X - differenceByX;
            }
            else
            {
                relativeCoordinates.X = differenceByX;
            }
            if (differenceByY < 0)
            {
                relativeCoordinates.Y = mainCoordinates.Y - differenceByY;
            }
            else
            {
                relativeCoordinates.Y = differenceByY;
            }

            return relativeCoordinates;
        }

        private void LoadImages()
        {
            bossImage = Image.FromFile(BossPath);
            bushImage = Image.FromFile(BushPath);
            chestImage = Image.FromFile(ChestPath);
            chooseFemaleWarrior = Image.FromFile(ChooseFemaleWarriorPath);
            chooseMagician = Image.FromFile(ChooseMagicianPath);
            chooseWarrior = Image.FromFile(ChooseWarriorPath);
            enemyImage = Image.FromFile(EnemyPath);
            femaleWarriorImage = Image.FromFile(FemaleWarriorPath);
            gameOver = Image.FromFile(GameOverPath);
            logo = Image.FromFile(LogoPath);
            magicianImage = Image.FromFile(MagicianPath);
            rockImage = Image.FromFile(RockPath);
            teleportImage = Image.FromFile(TeleportPath);
            treeImage = Image.FromFile(TreePath);
            warriorImage = Image.FromFile(WarriorPath);
            backgroundImage = Image.FromFile(BackgroundPath);
        }

        private void RenderFrame(Hero hero)
        {
            gameForm.BackgroundImage = backgroundImage;
            RenderTopBar(hero);
        }

        private void RenderTopBar(Hero hero)
        {
            var currentHealth = hero.CurrentHealth;
            var currentMana = hero.CurrentMana;
            var currentExperience = hero.CurrentExperience;
            var maxHealth = hero.MaxHealth;
            var maxMana = hero.MaxMana;
            var maxExperience = hero.MaxExperience;
            Image heroImage;

            switch (hero.HeroType)
            {
                case HeroType.FemaleWarrior:
                    heroImage = femaleWarriorImage;
                    break;
                case HeroType.Magician:
                    heroImage = magicianImage;
                    break;
                case HeroType.Warrior:
                    heroImage = warriorImage;
                    break;
                default:
                    throw new NotImplementedException("Hero view icon not implemented");
            }

            // Create hero icon

            var heroIcon = new PictureBox();
            heroIcon.Location = new Point(10, 10);
            heroIcon.Size = new Size(70, 70);
            heroIcon.Image = heroImage;
            heroIcon.BackgroundImage = heroImage;

            // Create health bar
            var healthBar = new ProgressBar();
            healthBar.BackColor = Color.Green;
            healthBar.BackColor = Color.Green;
            healthBar.Location = new Point(90, 10);
            healthBar.Size = new Size(200, 30);
            healthBar.Maximum = maxHealth;
            healthBar.Value = currentHealth;

            // Create mana bar
            var manaBar = new ProgressBar();
            manaBar.ForeColor = Color.DodgerBlue;
            manaBar.BackColor = Color.DodgerBlue;
            manaBar.Location = new Point(90, 10);
            manaBar.Size = new Size(200, 30);
            manaBar.Maximum = maxMana;
            manaBar.Value = currentMana;

            RenderTopBackground();
            this.gameForm.Controls.Add(heroIcon);
            this.gameForm.Controls.Add(healthBar);
            this.gameForm.Controls.Add(manaBar);
        }
        public void RenderStartScreen(GameForm gameForm)
        {
            gameForm.BackColor = Gray;
            PictureBox logoBox = CreatePictureBox(logo, 450, 200, new Point(gameForm.Height / 2 - 180, 100));
            gameForm.Controls.Add(logoBox);
            CreateMenuButtons();
            RenderButtons(this.MenuButtons, gameForm);
        }

        public void RenderChooseHeroScreen(GameForm gameForm)
        {
            CreateHeading("Choose Hero: ", gameForm.Width / 2 - 80, 40);
            PictureBox warriorBox = CreatePictureBox(chooseWarrior, 190, 250, new Point(10, 110));
            PictureBox femaleWarriorBox = CreatePictureBox(chooseFemaleWarrior, 190, 250, new Point(210, 110));
            PictureBox magicianBox = CreatePictureBox(chooseMagician, 190, 250, new Point(410, 110));
            gameForm.Controls.Add(warriorBox);
            gameForm.Controls.Add(femaleWarriorBox);
            gameForm.Controls.Add(magicianBox);
            CreateHeroesButtons();
            RenderButtons(this.HeroesButtons, gameForm);
        }

        public void RenderGameOverScreen()
        {
            PictureBox gameOverBox = CreatePictureBox(gameOver, 512, 58, new Point(gameForm.Height / 2 - 225, 150));
            gameForm.Controls.Add(gameOverBox);
            CreateMenuButtons();
            RenderButtons(menuButtons, gameForm);
        }

        private void RenderUnits(Units units)
        {
            renderHero(units.Hero);
            renderChests(units.Chests, units.Hero);
            renderObstacles(units.Obstacles, units.Hero);
            renderTeleports(units.Teleports, units.Hero);
            renderEnemies(units.Enemies, units.Hero);
        }

        private void renderEnemies(IDictionary<Coordinates, Enemy> enemies, Hero hero)
        {
            Coordinates heroCoordinates = hero.Coordinates;

            foreach (var enemy in enemies)
            {
                if (PictureBoxExists(enemy.Value))
                {
                    RedrawObject(enemy.Value, true);
                }
                else
                {
                    Coordinates relativeCoordinates = CalculateRelativeCoordinates(heroCoordinates, enemy.Key);

                    switch (enemy.Value.EnemyType)
                    {
                        case EnemyType.BossEnemy:
                            this.AddObject(bossImage, relativeCoordinates, enemy.Value, UnitOffset, true);
                            break;
                        case EnemyType.WeakEnemy:
                            this.AddObject(enemyImage, relativeCoordinates, enemy.Value, UnitOffset, true);
                            break;
                        default:
                            throw new NotImplementedException("Enemy view not implemented.");
                    }
                }
            }
        }

        private void renderTeleports(IDictionary<Coordinates, Teleportation> teleports, Hero hero)
        {
            Coordinates heroCoordinates = hero.Coordinates;

            foreach (var teleport in teleports)
            {
                if (PictureBoxExists(teleport.Value))
                {
                    RedrawObject(teleport.Value);
                }
                else
                {
                    Coordinates relativeCoordinates = CalculateRelativeCoordinates(heroCoordinates, teleport.Key);
                    this.AddObject(teleportImage, relativeCoordinates, teleport.Value, UnitOffset);
                }
            }
        }

        private void renderObstacles(IDictionary<Coordinates, Obstacle> obstacles, Hero hero)
        {
            Coordinates heroCoordinates = hero.Coordinates;

            foreach (var obstacle in obstacles)
            {
                Coordinates relativeCoordinates = CalculateRelativeCoordinates(heroCoordinates, obstacle.Key);
                if (PictureBoxExists(obstacle.Value))
                {
                    RedrawObject(obstacle.Value);
                }
                else
                {
                    switch (obstacle.Value.ObstacleType)
                    {
                        case ObstacleType.Bush:
                            this.AddObject(bushImage, relativeCoordinates, obstacle.Value, UnitOffset);
                            break;
                        case ObstacleType.Rock:
                            this.AddObject(rockImage, relativeCoordinates, obstacle.Value, UnitOffset);
                            break;
                        case ObstacleType.Tree:
                            this.AddObject(treeImage, relativeCoordinates, obstacle.Value, UnitOffset);
                            break;
                        default:
                            throw new NotImplementedException("Obstacle view not implemented.");
                    }
                }
            }
        }

        private void renderChests(IDictionary<Coordinates, Chest> chests, Hero hero)
        {
            Coordinates heroCoordinates = hero.Coordinates;

            foreach (var chest in chests)
            {
                if (PictureBoxExists(chest.Value))
                {
                    RedrawObject(chest.Value);
                    continue;
                }
                else
                {
                    Coordinates relativeCoordinates = CalculateRelativeCoordinates(heroCoordinates, chest.Key);
                    this.AddObject(chestImage, relativeCoordinates, chest.Value, UnitOffset);
                }
            }
        }

        private void renderHero(Hero hero)
        {
            Coordinates centerPointCoordinates = new Coordinates(4, 3);
            switch (hero.HeroType)
            {
                case HeroType.FemaleWarrior:
                    this.AddObject(femaleWarriorImage, centerPointCoordinates, hero, UnitOffset);
                    break;
                case HeroType.Warrior:
                    this.AddObject(warriorImage, centerPointCoordinates, hero, UnitOffset);
                    break;
                case HeroType.Magician:
                    //CreatePictureBoxForUnit(hero, femaleWarriorImage, centerPointCoordinates, UnitOffset);
                    this.AddObject(magicianImage, centerPointCoordinates, hero, UnitOffset);
                    break;
                default:
                    throw new NotImplementedException("Hero view not implemented.");
            }
        }

        private bool toRender(Coordinates heroCoordinates, Coordinates unitCoordinates)
        {
            // todo: constants
            bool collisionByX = unitCoordinates.X >= heroCoordinates.X - 4 &&
                                unitCoordinates.X <= heroCoordinates.X + 4;
            bool collisionByY = unitCoordinates.Y >= heroCoordinates.Y - 3 &&
                                unitCoordinates.Y <= heroCoordinates.Y + 3;
            if (collisionByX && collisionByY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //private void RenderHero(Hero hero)
        //{
        //    Coordinates centerPointCoordinates = new Coordinates(4, 3);
        //    switch (hero.HeroType)
        //    {
        //        case HeroType.FemaleWarrior:
        //            this.AddObject(femaleWarriorImage, centerPointCoordinates, hero, UnitOffset);
        //            break;
        //        case HeroType.Warrior:
        //            this.AddObject(warriorImage, centerPointCoordinates, hero, UnitOffset);
        //            break;
        //        case HeroType.Magician:
        //            //CreatePictureBoxForUnit(hero, femaleWarriorImage, centerPointCoordinates, UnitOffset);
        //            this.AddObject(magicianImage, centerPointCoordinates, hero, UnitOffset);
        //            break;
        //        default:
        //            throw new NotImplementedException("Hero view not implemented.");
        //    }
        //}

        //private void RenderEnemies(IDictionary<Coordinates, Enemy> enemies, Hero hero)
        //{
        //    Coordinates heroCoordinates = hero.Coordinates;
        //    foreach (var enemy in enemies)
        //    {
        //        if (toRender(heroCoordinates, enemy.Key))
        //        {
        //            if (PictureBoxExists(enemy.Value))
        //            {
        //                RedrawObject(enemy.Value, true);
        //            }
        //            else
        //            {
        //                Coordinates relativeCoordinates =
        //                    CalculateRelativeCoordinates(heroCoordinates, enemy.Key);
        //                RenderObject.RenderProgressBar(gameForm, enemy.Value.MaxHealth,
        //                    enemy.Value.CurrentHealth, enemy.Key,
        //                    Color.Green, ProgressBarOffset);

        //                switch (enemy.Value.EnemyType)
        //                {
        //                    case EnemyType.BossEnemy:
        //                        this.AddObject(bossImage, relativeCoordinates, enemy.Value, UnitOffset, true);
        //                        break;
        //                    case EnemyType.WeakEnemy:
        //                        this.AddObject(enemyImage, relativeCoordinates, enemy.Value, UnitOffset, true);
        //                        break;
        //                    default:
        //                        throw new NotImplementedException("Enemy view not implemented.");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (PictureBoxExists(enemy.Value))
        //            {
        //                RemoveObject(enemy.Value);
        //            }
        //        }
        //    }
        //}

        //private void RenderTeleports(IDictionary<Coordinates, Teleportation> teleports, Hero hero)
        //{
        //    Coordinates heroCoordinates = hero.Coordinates;
        //    foreach (var teleport in teleports)
        //    {
        //        if (toRender(hero.Coordinates, teleport.Key))
        //        {
        //            if (PictureBoxExists(teleport.Value))
        //            {
        //                RedrawObject(teleport.Value);
        //            }
        //            else
        //            {
        //                Coordinates relativeCoordinates = CalculateRelativeCoordinates(heroCoordinates, teleport.Key);
        //                this.AddObject(teleportImage, relativeCoordinates, teleport.Value, UnitOffset);
        //            }
        //        }
        //        else
        //        {
        //            if (PictureBoxExists(teleport.Value))
        //            {
        //                RemoveObject(teleport.Value);
        //            }
        //        }
        //    }
        //}

        //private void RenderObstacles(IDictionary<Coordinates, Obstacle> obstacles, Hero hero)
        //{
        //    Coordinates heroCoordinates = hero.Coordinates;
        //    foreach (var obstacle in obstacles)
        //    {
        //        if (toRender(hero.Coordinates, obstacle.Key))
        //        {
        //            if (PictureBoxExists(obstacle.Value))
        //            {
        //                RedrawObject(obstacle.Value);
        //            }
        //            else
        //            {
        //                Coordinates relativeCoordinates = CalculateRelativeCoordinates(heroCoordinates, obstacle.Key);

        //                switch (obstacle.Value.ObstacleType)
        //                {
        //                    case ObstacleType.Bush:
        //                        this.AddObject(bushImage, relativeCoordinates, obstacle.Value, UnitOffset);
        //                        break;
        //                    case ObstacleType.Rock:
        //                        this.AddObject(rockImage, relativeCoordinates, obstacle.Value, UnitOffset);
        //                        break;
        //                    case ObstacleType.Tree:
        //                        this.AddObject(treeImage, relativeCoordinates, obstacle.Value, UnitOffset);
        //                        break;
        //                    default:
        //                        throw new NotImplementedException("Obstacle view not implemented.");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (PictureBoxExists(obstacle.Value))
        //            {
        //                RemoveObject(obstacle.Value);
        //            }
        //        }
        //    }
        //}

        //private void RenderChests(IDictionary<Coordinates, Chest> chests, Hero hero)
        //{
        //    Coordinates heroCoordinates = hero.Coordinates;
        //    foreach (var chest in chests)
        //    {
        //        if (toRender(hero.Coordinates, chest.Key))
        //        {
        //            if (PictureBoxExists(chest.Value))
        //            {
        //                RedrawObject(chest.Value);
        //            }
        //            else
        //            {
        //                Coordinates relativeCoordinates = CalculateRelativeCoordinates(heroCoordinates, chest.Key);
        //                this.AddObject(chestImage, relativeCoordinates, chest.Value, UnitOffset);
        //            }
        //        }
        //        else
        //        {
        //            if (PictureBoxExists(chest.Value))
        //            {
        //                RemoveObject(chest.Value);
        //            }
        //        }
        //    }
        //}

        private Button CreateButton(string text, int top, int left)
        {
            Button button = new Button();
            button.Text = text;
            button.Top = top;
            button.Left = left;
            return button;
        }

        private void CreateMenuButtons()
        {
            Button play = CreateButton("Play", gameForm.Height / 2 + 20, gameForm.Width / 2 - 75);
            this.MenuButtons.Add(play);
            play.Click += new EventHandler(gameForm.PlayClick);

            Button exit = CreateButton("Exit", play.Top + 40, gameForm.Width / 2 - 75);
            this.MenuButtons.Add(exit);
            exit.Click += new EventHandler(gameForm.ExitClick);
        }

        private void CreateHeroesButtons()
        {
            Button warriorButton = CreateButton("The Warrior", gameForm.Height - 150, 30);
            this.HeroesButtons.Add(warriorButton);
            warriorButton.Click += new EventHandler(gameForm.WarriorClick);

            Button femaleWarriorButton = CreateButton("The Iron Lady", warriorButton.Top, warriorButton.Left + 200);
            this.HeroesButtons.Add(femaleWarriorButton);
            femaleWarriorButton.Click += new EventHandler(gameForm.FemaleWarriorClick);

            Button magicianButton = CreateButton("The Wizard", warriorButton.Top, femaleWarriorButton.Left + 200);
            this.HeroesButtons.Add(magicianButton);
            magicianButton.Click += new EventHandler(gameForm.MagicianClick);
        }

        private void RenderButtons(List<Button> buttons, GameForm gameForm)
        {
            foreach (var button in buttons)
            {
                button.Size = ButtonSize;
                button.Font = FontFamily;
                button.TabStop = false;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.BackColor = Yellow;
                button.ForeColor = Gray;
                gameForm.Controls.Add(button);
            }
        }

        private void CreateHeading(string text, int width, int height)
        {
            Graphics formGraphics = gameForm.CreateGraphics();
            string drawString = text;
            Font drawFont = HeadingFont;
            SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Black);
            StringFormat drawFormat = new StringFormat();
            formGraphics.DrawString(drawString, drawFont, drawBrush, width, height, drawFormat);
        }

        public static PictureBox CreatePictureBox(Image image, int width,
            int height, Point position)
        {
            PictureBox box = new PictureBox();
            box.Image = image;
            box.Location = position;
            box.Size = new Size(width, height);
            return box;
        }

        public void AddObject(Image image, Coordinates coordinates,
            Unit renderableObject, Coordinates offset, bool addProgressBar = false)
        {
            RenderObject.RenderImage(this.gameForm, image, coordinates, renderableObject, offset);
            if (addProgressBar)
            {
                this.CreateProgressBar(renderableObject as LivingUnit);
            }
        }

        public void RemoveObject(Unit renderableObject)
        {
            if (PictureBoxExists(renderableObject))
            {
                var picBox = GetPictureBoxByObject(renderableObject);
                this.gameForm.Controls.Remove(picBox);
                RendererView.pictureBoxes.Remove(picBox);
                if (renderableObject is LivingUnit)
                {
                    var progressBar = GetProgressBarByObject(renderableObject as LivingUnit);
                    this.gameForm.Controls.Remove(progressBar);
                    RendererView.progressBars.Remove(progressBar);
                }
            }
        }

        public void RedrawObject(Unit objectToBeRedrawn, bool hasProgressBar = false)
        {
            var newCoordinates = new Point(objectToBeRedrawn.Coordinates.X * 70, objectToBeRedrawn.Coordinates.Y * 70);
            var picBox = GetPictureBoxByObject(objectToBeRedrawn);
            picBox.Location = newCoordinates;

            if (hasProgressBar)
            {
                var unit = objectToBeRedrawn as Enemy;
                var progressBar = GetProgressBarByObject(unit);
                this.SetProgressBarLocation(unit, progressBar);
                progressBar.Value = unit.CurrentHealth;
            }
        }

        private PictureBox GetPictureBoxByObject(Unit renderableObject)
        {
            if (PictureBoxExists(renderableObject))
            {
                return RendererView.pictureBoxes.First(p => p.Tag == renderableObject);
            }
            return new PictureBox();
        }

        private bool PictureBoxExists(Unit renderableObject)
        {
            return RendererView.pictureBoxes.Exists(p => p.Tag == renderableObject);
        }

        private ProgressBar GetProgressBarByObject(Unit unit)
        {
            return RendererView.progressBars.First(p => p.Tag == unit);
        }

        private void SetProgressBarLocation(LivingUnit unit, ProgressBar progressBar)
        {
            progressBar.Location = new Point(unit.Coordinates.X * 70, unit.Coordinates.Y * 70 - 15);
        }

        private void CreateProgressBar(LivingUnit unit)
        {
            var progressBar = new ProgressBar();
            progressBar.Size = new Size(70, 15);
            this.SetProgressBarLocation(unit, progressBar);
            progressBar.Maximum = unit.MaxHealth;
            progressBar.Value = unit.CurrentHealth;
            progressBar.Tag = unit;
            progressBars.Add(progressBar);
            this.gameForm.Controls.Add(progressBar);
        }
    }
}