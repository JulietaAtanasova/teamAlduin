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
        private Color Gray = Color.FromArgb(0, 61, 55, 55);
        private Font FontFamily = new Font("Sans-serif", 12, FontStyle.Bold);
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

        private List<Button> buttons = new List<Button>();

        public RendererView(GameForm gameForm)
        {
            this.gameForm = gameForm;
            LoadImages();
            RendererView.pictureBoxes = new List<PictureBox>();
            RendererView.progressBars = new List<ProgressBar>();
            this.unitsToRender = new List<Unit>();
        }

        public List<Button> Buttons
        {
            get
            {
                return this.buttons;
            }
        }

        public void Render(Units units, GameMap gameMap)
        {
            ClearScreen(units.Hero);
            RenderFrame();
            RenderUnits(units);
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
            foreach (var unit in unitsToRemove)
            {
                RemoveObject(unit);
            }
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
            enemyImage = Image.FromFile(EnemyPath);
            femaleWarriorImage = Image.FromFile(FemaleWarriorPath);
            magicianImage = Image.FromFile(MagicianPath);
            rockImage = Image.FromFile(RockPath);
            teleportImage = Image.FromFile(TeleportPath);
            treeImage = Image.FromFile(TreePath);
            warriorImage = Image.FromFile(WarriorPath);
            backgroundImage = Image.FromFile(BackgroundPath);
        }

        private void RenderFrame()
        {
            gameForm.BackgroundImage = backgroundImage;
        }
        public void RenderStartScreen(GameForm gameForm)
        {
            gameForm.BackColor = Gray;
            PictureBox logoBox = CreatePictureBox(logo, 450, 200, new Point(gameForm.Height / 2 - 60, 200));
            gameForm.Controls.Add(logoBox);
            CreateButtons();
            RenderButtons(buttons, gameForm);
        } 

        public void RenderGameOverScreen()
        {
            PictureBox gameOverBox = CreatePictureBox(gameOver, 512, 58, new Point(gameForm.Height / 2 - 100, 200));
            gameForm.Controls.Add(gameOverBox);
        }

        private void RenderUnits(Units units)
        {
            this.RenderHero(units.Hero);
            this.RenderEnemies(units.Enemies, units.Hero);
            this.RenderTeleports(units.Teleports, units.Hero);
            this.RenderObstacles(units.Obstacles, units.Hero);
            this.RenderChests(units.Chests, units.Hero);
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

        private void RenderHero(Hero hero)
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

        private void RenderEnemies(IDictionary<Coordinates, Enemy> enemies, Hero hero)
        {
            Coordinates heroCoordinates = hero.Coordinates;
            foreach (var enemy in enemies)
            {
                if (toRender(heroCoordinates, enemy.Key))
                {
                    Coordinates relativeCoordinates = CalculateRelativeCoordinates(heroCoordinates, enemy.Key);
                    RenderObject.RenderProgressBar(gameForm, enemy.Value.MaxHealth, enemy.Value.CurrentHealth, enemy.Key,
                        Color.Green, ProgressBarOffset);
                    if (PictureBoxExists(enemy.Value))
                    {
                        RedrawObject(enemy.Value);
                        continue;
                    }
                    switch (enemy.Value.EnemyType)
                    {
                        case EnemyType.BossEnemy:
                            this.AddObject(bossImage, relativeCoordinates, enemy.Value, UnitOffset);
                            break;
                        case EnemyType.WeakEnemy:
                            this.AddObject(enemyImage, relativeCoordinates, enemy.Value, UnitOffset);
                            break;
                        default:
                            throw new NotImplementedException("Enemy view not implemented.");
                    }
                }
            }
        }

        private void RenderTeleports(IDictionary<Coordinates, Teleportation> teleports, Hero hero)
        {
            Coordinates heroCoordinates = hero.Coordinates;
            foreach (var teleport in teleports)
            {
                if (toRender(hero.Coordinates, teleport.Key))
                {
                    if (PictureBoxExists(teleport.Value))
                    {
                        RedrawObject(teleport.Value);
                        continue;
                    }
                    Coordinates relativeCoordinates = CalculateRelativeCoordinates(heroCoordinates, teleport.Key);
                    this.AddObject(teleportImage, relativeCoordinates, teleport.Value, UnitOffset);
                }
            }
        }

        private void RenderObstacles(IDictionary<Coordinates, Obstacle> obstacles, Hero hero)
        {
            Coordinates heroCoordinates = hero.Coordinates;
            foreach (var obstacle in obstacles)
            {
                if (toRender(hero.Coordinates, obstacle.Key))
                {
                    Coordinates relativeCoordinates = CalculateRelativeCoordinates(heroCoordinates, obstacle.Key);
                    if (PictureBoxExists(obstacle.Value))
                    {
                        RedrawObject(obstacle.Value);
                        continue;
                    }
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

        private void RenderChests(IDictionary<Coordinates, Chest> chests, Hero hero)
        {
            Coordinates heroCoordinates = hero.Coordinates;
            foreach (var chest in chests)
            {
                if (toRender(hero.Coordinates, chest.Key))
                {
                    if (PictureBoxExists(chest.Value))
                    {
                        RedrawObject(chest.Value);
                        continue;
                    }
                    Coordinates relativeCoordinates = CalculateRelativeCoordinates(heroCoordinates, chest.Key);
                    this.AddObject(chestImage, relativeCoordinates, chest.Value, UnitOffset);
                }
            }
        }

        private void CreateButtons()
        {
            Button play = new Button();
            play.Text = "Play";
            play.Top = gameForm.Height / 2 + 80;
            this.Buttons.Add(play);
            play.Click += new EventHandler(gameForm.PlayClick);

            Button exit = new Button();
            exit.Text = "Exit";
            exit.Top = gameForm.Height / 2 + 120;
            this.Buttons.Add(exit);
            exit.Click += new EventHandler(gameForm.ExitClick);
        }

        private void RenderButtons(List<Button> buttons, GameForm gameForm)
        {
            foreach (var button in buttons)
            {
                button.Size = ButtonSize;
                button.Left = gameForm.Width / 2 - button.Width / 2;
                button.Font = FontFamily;
                button.TabStop = false;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.BackColor = Yellow;
                button.ForeColor = Gray;
                gameForm.Controls.Add(button);
            }
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

        public void AddObject(Image image, Coordinates coordinates, Unit renderableObject, Coordinates offset)
        {
            RenderObject.RenderImage(this.gameForm, image, coordinates, renderableObject, offset);
            if (renderableObject is LivingUnit)
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

        public void RedrawObject(Unit objectToBeRedrawn)
        {
            var newCoordinates = new Point(objectToBeRedrawn.Coordinates.X * 70, objectToBeRedrawn.Coordinates.Y * 70);
            var picBox = GetPictureBoxByObject(objectToBeRedrawn);
            picBox.Location = newCoordinates;

            if (objectToBeRedrawn is LivingUnit)
            {
                var unit = objectToBeRedrawn as LivingUnit;
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
            progressBar.Location = new Point(unit.Coordinates.X * 70 - ProgressBarOffset.X, unit.Coordinates.Y * 70 - ProgressBarOffset.Y);
        }

        private void CreateProgressBar(LivingUnit unit)
        {
            var progressBar = new ProgressBar();
            progressBar.Size = new Size(ProgressBarOffset.X, ProgressBarOffset.Y);
            this.SetProgressBarLocation(unit, progressBar);
            progressBar.Maximum = unit.MaxHealth;
            progressBar.Value = unit.CurrentHealth;
            progressBar.Tag = unit;
            progressBars.Add(progressBar);
            this.gameForm.Controls.Add(progressBar);
        }
    }
}