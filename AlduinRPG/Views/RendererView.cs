using System;

namespace AlduinRPG.Views
{
    using Interfaces;
    using System.Collections.Generic;
    using Models;
    using System.Drawing;

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
        
        public RendererView(GameForm gameForm)
        {
            this.gameForm = gameForm;
        }

        public void Render(Dictionary<Coordinates, IUnit> units, GameMap gameMap)
        {
            LoadImages();
            RenderFrame();
            RenderUnits(units);
            // TODO: clear objects 
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
            // TODO
        }

        private void RenderUnits(Dictionary<Coordinates, IUnit> units)
        {
            foreach (var unit in units)
            {
                var unitType = unit.Value.GetType().Name;
                var coordinates = unit.Key;

                switch (unitType)
                {
                    case "Warrior":
                        RenderObject.RenderImage(gameForm, warriorImage, coordinates);
                        break;
                    case "Magician":
                        RenderObject.RenderImage(gameForm, magicianImage, coordinates);
                        break;
                    case "FemaleWarrior":
                        RenderObject.RenderImage(gameForm, femaleWarriorImage, coordinates);
                        break;
                    case "BossEnemy":
                        RenderObject.RenderImage(gameForm, bossImage, coordinates);
                        break;
                    case "WeakEnemy":
                        RenderObject.RenderImage(gameForm, enemyImage, coordinates);
                        break;
                    case "Chest":
                        RenderObject.RenderImage(gameForm, chestImage, coordinates);
                        break;
                    case "Obstacle":
                        RenderObject.RenderImage(gameForm, treeImage, coordinates);
                        break;
                    case "Rock":
                        RenderObject.RenderImage(gameForm, rockImage, coordinates);
                        break;
                    case "Bush":
                        RenderObject.RenderImage(gameForm, bushImage, coordinates);
                        break;
                    case "Teleport":
                        RenderObject.RenderImage(gameForm, teleportImage, coordinates);
                        break;
                    default:
                        throw new NotImplementedException(String.Format("Unit type view not implemented ({0}).", unitType));
                        break;
                }
            }
        }
    }
}