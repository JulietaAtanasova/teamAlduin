using System.Drawing;

namespace AlduinRPG.Views
{
    using Interfaces;
    using System.Collections.Generic;
    using Models;
    using System.Windows.Forms;
    using System.Linq;

    public class RendererView
    {
        private GameForm gameForm;
        private Hero hero;
        

        public RendererView(GameForm gameForm)
        {
            this.gameForm = gameForm;
        }

        public void Render(Dictionary<Coordinates, IUnit> units, GameMap gameMap)
        {
            Hero hero = GetHeroData(units);
            RenderFrame();
            RenderUnits(units);
        }

        private void RenderFrame()
        {
            // TODO
        }

        private void RenderUnits(Dictionary<Coordinates, IUnit> units)
        {
            foreach (var unit in units)
            {
                var unitType = unit.GetType().Name;
                var coordinates = unit.Key;

                RenderObject.RenderImage(gameForm, "Resources/teleport.png", 0, 0, 80, 69);
                ;
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
            }
        }
        
        private static Hero GetHeroData(Dictionary<Coordinates, IUnit> units)
        {
            //var data = units.First(unit => unit is Hero).Value as Hero;
            //return data;
            return null;
        } 
    }
}
