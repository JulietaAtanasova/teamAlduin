using System.Collections;
using System.Collections.Generic;
using AlduinRPG.Interfaces;

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
        private const string backgroundPath = "../../Resources/grass70x70.png";

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
        private Image backgroundImage;

        private readonly Coordinates ProgressBarOffset = new Coordinates(15, 30);
        private readonly Coordinates UnitOffset = new Coordinates(70, 70);

        public RendererView(GameForm gameForm)
        {
            this.gameForm = gameForm;
            LoadImages();
        }

        public void Render(Units units, GameMap gameMap)
        {
            ClearScreen();
            RenderFrame();
            RenderUnits(units);
        }

        private void ClearScreen()
        {
            gameForm.Controls.Clear();
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
            backgroundImage = Image.FromFile(backgroundPath);
        }

        private void RenderFrame()
        {
            gameForm.BackgroundImage = backgroundImage;
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
            if (collisionByX && collisionByY || true)
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
            switch (hero.HeroType)
            {
                case HeroType.FemaleWarrior:
                    RenderObject.RenderImage(gameForm, femaleWarriorImage, hero.Coordinates, UnitOffset);
                    break;
                case HeroType.Warrior:
                    RenderObject.RenderImage(gameForm, warriorImage, new Coordinates(4, 3), UnitOffset);
                    break;
                case HeroType.Magician:
                    RenderObject.RenderImage(gameForm, magicianImage, hero.Coordinates, UnitOffset);
                    break;
                default:
                    throw new NotImplementedException("Hero view not implemented.");
            }
        }

        private void RenderEnemies(Dictionary<Coordinates, Enemy> enemies, Hero hero)
        {
            foreach (var enemy in enemies)
            {
                if (toRender(hero.Coordinates, enemy.Key))
                {
                    RenderObject.RenderProgressBar(gameForm, enemy.Value.MaxHealth, enemy.Value.CurrentHealth, enemy.Key, Color.Green, ProgressBarOffset);
                    switch (enemy.Value.EnemyType)
                    {
                        case EnemyType.BossEnemy:
                            RenderObject.RenderImage(gameForm, bossImage, enemy.Key, UnitOffset);
                            break;
                        case EnemyType.WeakEnemy:
                            RenderObject.RenderImage(gameForm, enemyImage, enemy.Key, UnitOffset);
                            break;
                        default:
                            throw new NotImplementedException("Enemy view not implemented.");
                    }
                }
            }
        }

        private void RenderTeleports(Dictionary<Coordinates, Teleportation> teleports, Hero hero)
        {
            foreach (var teleport in teleports)
            {
                if (toRender(hero.Coordinates, teleport.Key))
                {
                    RenderObject.RenderImage(gameForm, teleportImage, teleport.Key, UnitOffset);
                }
            }
        }

        private void RenderObstacles(Dictionary<Coordinates, Obstacle> obstacles, Hero hero)
        {
            foreach (var obstacle in obstacles)
            {
                if (toRender(hero.Coordinates, obstacle.Key))
                {
                    switch (obstacle.Value.ObstacleType)
                    {
                        case ObstacleType.Bush:
                            RenderObject.RenderImage(gameForm, bushImage, obstacle.Key, UnitOffset);
                            break;
                        case ObstacleType.Rock:
                            RenderObject.RenderImage(gameForm, rockImage, obstacle.Key, UnitOffset);
                            break;
                        case ObstacleType.Tree:
                            RenderObject.RenderImage(gameForm, treeImage, obstacle.Key, UnitOffset);
                            break;
                        default:
                            throw new NotImplementedException("Obstacle view not implemented.");
                    }
                }
            }
        }

        private void RenderChests(Dictionary<Coordinates, Chest> chests, Hero hero)
        {
            foreach (var chest in chests)
            {
                if (toRender(hero.Coordinates, chest.Key))
                {
                    RenderObject.RenderImage(gameForm, chestImage, chest.Key, UnitOffset);
                }
            }
        }
    }
}