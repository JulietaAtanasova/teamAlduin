namespace AlduinRPG
{
    using System;
    using System.Windows.Forms;
    using Models;

    public partial class GameForm : Form
    {
        public const int TimeInterval = 150;
        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            GameMap gameMap = new GameMap(MapType.Big);
            var engine = new Engine.Engine(this, gameMap);

            Timer timer = new Timer();
            timer.Interval = TimeInterval;
            timer.Tick += (s, args) => engine.Run();
            timer.Start();
        }
    }
}
