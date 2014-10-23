namespace AlduinRPG.Views
{
    using Models;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class PaintBrush
    {
        private GameForm gameForm;
        private Units unitsToRender;
        public static List<PictureBox> pictureBoxes;
        public List<ProgressBar> progressBars = new List<ProgressBar>();
        private const string BossPath = "../../Resources/boss70x70.png";
        private const string BushPath = "../../Resources/bush.png";
        private const string ChestPath = "../../Resources/chest70x70.png";
        private const string ChooseFemaleWarriorPath = "../../Resources/chooseFemaleWarrior.png";
        private const string ChooseMagicianPath = "../../Resources/chooseMagician.png";
        private const string ChooseWarriorPath = "../../Resources/chooseWarrior.png";
        private const string EnemyPath = "../../Resources/enemy70x70.png";
        private const string FemaleWarriorPath = "../../Resources/female-warrior70x70.png";
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
        private Image logo;
        private Image magicianImage;
        private Image rockImage;
        private Image teleportImage;
        private Image treeImage;
        private Image warriorImage;
        private Image backgroundImage;

        private readonly Coordinates ProgressBarOffset = new Coordinates(0, 15);
        private const int TopBarOffset = 90;
        private readonly Coordinates UnitOffset = new Coordinates(70, 70);

        public PaintBrush(GameForm gameForm)
        {
            this.gameForm = gameForm;
            LoadImages();
            RendererView.pictureBoxes = new List<PictureBox>();
            RendererView.progressBars = new List<ProgressBar>();
            this.unitsToRender = new Units();
        }

        public void Render(Units units, GameMap gameMap)
        {
            ClearScreen();
            unitsToRender = getUnitsToRender(units);
            RenderFrame();
            RenderUnits(unitsToRender);
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
                    RedrawObject(enemy.Value);
                }
                else
                {
                    Coordinates relativeCoordinates = CalculateRelativeCoordinates(heroCoordinates, enemy.Key);

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

        private void RenderFrame()
        {
            gameForm.BackgroundImage = backgroundImage;
        }

        private void ClearScreen()
        {

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
            return this.progressBars.First(p => p.Tag == unit);
        }

        private void SetProgressBarLocation(LivingUnit unit, ProgressBar progressBar)
        {
            progressBar.Location = new Point(
                (unit.Coordinates.X * 70) - ProgressBarOffset.X,
                TopBarOffset + (unit.Coordinates.Y * 70) - ProgressBarOffset.Y);
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

        public void MoveObjects(Units units)
        {
            RedrawObject(units.Hero);
            //units.Enemies.Values.ToList().ForEach(RedrawObject);
            foreach (var enemy in units.Enemies)
            {
                RedrawObject(enemy.Value);
            }
            //units.Obstacles.Values.ToList().ForEach(RedrawObject);
            //units.Teleports.Values.ToList().ForEach(RedrawObject);
            //units.Chests.Values.ToList().ForEach(RedrawObject);
        }
    }
}
