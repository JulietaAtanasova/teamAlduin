using AlduinRPG.Interfaces;
using AlduinRPG.Views;

namespace AlduinRPG
{
    using System;
    using System.Windows.Forms;
    using Models;

    public partial class GameForm : Form
    {
        public const int TimeInterval = 1000;
        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            IUserInput controller = new KeyboardController(this);
            GameMap gameMap = new GameMap(MapType.Small);
            var engine = new Engine.Engine(this, gameMap, controller);

            Timer timer = new Timer();
            timer.Interval = TimeInterval;
            timer.Enabled = true;
            timer.Tick += (s, args) => engine.Run();
            timer.Start();
        }
    }
}
