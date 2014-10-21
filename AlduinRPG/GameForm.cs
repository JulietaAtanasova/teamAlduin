namespace AlduinRPG
{
    using System;
    using System.Windows.Forms;
    using Models;

    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            GameMap gameMap = new GameMap(MapType.Medium);
            var engine = new Engine.Engine(this, gameMap);
            engine.Run();
        }
    }
}
