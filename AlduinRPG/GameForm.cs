using AlduinRPG.Interfaces;
using AlduinRPG.Views;

namespace AlduinRPG
{
    using System;
    using System.Windows.Forms;
    using Models;

    public partial class GameForm : Form
    {
        public const int TimeInterval = 500;

        public GameForm()
        {
            InitializeComponent();
        }

        public void MagicianClick(object sender, EventArgs e)
        {
            this.Controls.Clear();
            PlayGame(HeroType.Magician);
        }

        public void FemaleWarriorClick(object sender, EventArgs e)
        {
            this.Controls.Clear();
            PlayGame(HeroType.FemaleWarrior);
        }

        public void WarriorClick(object sender, EventArgs e)
        {
            this.Controls.Clear();
            PlayGame(HeroType.Warrior);
        }

        internal void PlayClick(object sender, EventArgs e)
        {
            this.Controls.Clear();
            var render = new RendererView(this);
            render.RenderChooseHeroScreen(this);
        }

        internal void ExitClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Confirm",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                  == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            var render = new RendererView(this);
            render.RenderStartScreen(this);
        }

        private void PlayGame(HeroType hero)
        {
            GameMap gameMap = new GameMap(MapType.Small);
            IUserInput controller = new KeyboardController(this);
            var engine = new Engine.Engine(this, gameMap, controller, hero);
            Timer timer = new Timer();
            timer.Interval = TimeInterval;
            timer.Enabled = true;
            timer.Tick += (s, args) => engine.Run();
            timer.Start();
        }
    }
}
