namespace AlduinRPG.Views
{
    using Models;
    using System.Drawing;
    using System;

    public class RendererView
    {
        private GameForm gameForm;
        private const string BossPath = "../../Resources/boss70x70.png";
        private const string BushPath = "../../Resources/bush.png";
        private const string ChestPath = "../../Resources/chest70x70.png";
        private const string EnemyPath = "../../Resources/enemy70x70.png";
        private const string FemaleWarriorPath = "../../Resources/female-warrior70x70.png";
        private const string MagicianPath = "../../Resources/magician70x70.png";
        private const string RockPath = "../../Resources/rock70x70.png";
        private const string TeleportPath = "../../Resources/teleport.png";
        private const string TreePath = "../../Resources/tree.png";
        private const string WarriorPath = "../../Resources/warrior70x70.png";

        private Image bossImage;
        private Image bushImage;
        private Image chestImage;
        private Image enemyImage;
        private Image femaleWarriorImage;
        private Image magicianImage;
        private Image rockImage;
        private Image teleportImage;
        private Image treeImage;
        private Image warriorImage;

        private readonly Coordinates ProgressBarOffset = new Coordinates(15, 30);

        public RendererView(GameForm gameForm)
        {
            this.gameForm = gameForm;
        }

        public void Render(Units units, GameMap gameMap)
        {
            LoadImages();
            RenderFrame();
            RenderUnits(units);
            //gameForm.Controls.Clear();
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
        }

        private void RenderFrame()
        {
            Image background = Image.FromFile("../../Resources/grass70x70.png");
            gameForm.BackgroundImage = background;
        }

        private void RenderUnits(Units units)
        {
            // Render hero
            switch (units.Hero.HeroType)
            {
                case HeroType.FemaleWarrior:
                    RenderObject.RenderImage(gameForm, femaleWarriorImage, units.Hero.Coordinates);
                    break;
                case HeroType.Warrior:
                    RenderObject.RenderImage(gameForm, warriorImage, units.Hero.Coordinates);
                    break;
                case HeroType.Magician:
                    RenderObject.RenderImage(gameForm, magicianImage, units.Hero.Coordinates);
                    break;
                default:
                    throw new NotImplementedException("Hero view not implemented.");
            }

            // Render enemies.
            foreach (var enemy in units.Enemies)
            {
                switch (enemy.Value.EnemyType)
                {
                    case EnemyType.BossEnemy:
                        RenderObject.RenderImage(gameForm, bossImage, enemy.Key);
                        break;
                    case EnemyType.WeakEnemy:
                        RenderObject.RenderImage(gameForm, enemyImage, enemy.Key);
                        break;
                    default:
                        throw new NotImplementedException("Enemy view not implemented.");
                }
            }

            // Render each teleport
            foreach (var teleport in units.Teleports)
            {
                RenderObject.RenderImage(gameForm, teleportImage, teleport.Key);
            }

            // Render obstacles
            foreach (var obstacle in units.Obstacles)
            {
                switch (obstacle.Value.ObstacleType)
                {
                    case ObstacleType.Bush:
                        RenderObject.RenderImage(gameForm, bushImage, obstacle.Key);
                        break;
                    case ObstacleType.Rock:
                        RenderObject.RenderImage(gameForm, rockImage, obstacle.Key);
                        break;
                    case ObstacleType.Tree:
                        RenderObject.RenderImage(gameForm, treeImage, obstacle.Key);
                        break;
                    default:
                        throw new NotImplementedException("Obstacle view not implemented.");
                }
            }

            // Render chests
            foreach (var chest in units.Chests)
            {
                RenderObject.RenderImage(gameForm, chestImage, chest.Key);
            }
        }
    }
}