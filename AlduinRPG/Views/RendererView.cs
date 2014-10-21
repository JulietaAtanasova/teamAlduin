using System.Drawing;
using System.Xml.Serialization;
using Telerik.WinControls.UI;

namespace AlduinRPG.Views
{
    using Interfaces;
    using System.Collections.Generic;
    using Models;

    public class RendererView
    {
        private GameForm gameForm;
        private Hero hero;
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
            Hero hero = GetHeroData(units);
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
            RenderObject.RenderImage(gameForm, teleportImage, 0, 0, 80, 69);
            //foreach (var unit in units)
            //{
            //    var unitType = unit.GetType().Name;
            //    var coordinates = unit.Key;

                //switch (unitType)
                //{
                //    case "Warrior":
                //        WarriorView warriorView = new WarriorView();
                //        warriorView.Render(coordinates);
                //        break;
                //    case "Magician":
                //        MagicianView magicianView = new MagicianView();
                //        magicianView.Render(coordinates);
                //        break;
                //    case "BossEnemy":
                //        BossView bossBiew = new BossView();
                //        bossBiew.Render(coordinates);
                //        break;
                //    case "WeakEnemy":
                //        EnemyView enemyView = new EnemyView();
                //        enemyView.Render(coordinates);
                //        break;
                //    case "Chest":
                //        ChestView chestView = new ChestView();
                //        chestView.Render(coordinates);
                //        break;
                //    case "Tree":
                //        TreeView treeView = new TreeView();
                //        treeView.Render(coordinates);
                //        break;
                //    case "Rock":
                //        RockView rockView = new RockView();
                //        rockView.Render(coordinates);
                //        break;
                //    case "Bush":
                //        BushView bushView = new BushView();
                //        bushView.Render(coordinates);
                //        break;
                //    default:
                //        break;
                //}
            //}
        }
        
        private static Hero GetHeroData(Dictionary<Coordinates, IUnit> units)
        {
            //var data = units.First(unit => unit is Hero).Value as Hero;
            //return data;
            return null;
        } 
    }
}