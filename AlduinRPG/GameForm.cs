namespace AlduinRPG
{
    using Views;
    using System;
    using System.Windows.Forms;

    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            RendererView painter = new RendererView(this);

        }
    }
}
