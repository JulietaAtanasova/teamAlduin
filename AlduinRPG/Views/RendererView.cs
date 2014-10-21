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
        private string bossPath,
            bushPath,
            chestPath,
            enemyPath,
            magicianPath,
            rockPath,
            teleportPath = "../../Resources/teleport.png",
            treePath,
            warriorPath;

        private Image bossImage,
            bushImage,
            chestImage,
            enemyImage,
            magicianImage,
            rockImage,
            teleportImage,
            treeImage,
            warriorImage;
        
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
        }

        private void LoadImages()
        {
            //bossImage = Image.FromFile(bossPath);
            //bushImage = Image.FromFile(bushPath);
            //chestImage = Image.FromFile(chestPath);
            //enemyImage = Image.FromFile(enemyPath);
            //magicianImage = Image.FromFile(magicianPath);
            //rockImage = Image.FromFile(rockPath);
            teleportImage = Image.FromFile(teleportPath);
            //treeImage = Image.FromFile(treePath);
            //warriorImage = Image.FromFile(warriorPath);
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
