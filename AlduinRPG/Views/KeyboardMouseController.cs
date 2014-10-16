
namespace AlduinRPG.Views
{
    using Interfaces;
    using System;
    using System.Windows.Forms;

    public partial class KeyboardMouseController : Telerik.WinControls.UI.RadForm, IUserInputInterface
    {
        public event EventHandler OnRightPressed;

        public event EventHandler OnLeftPressed;

        public event EventHandler OnUpPressed;

        public event EventHandler OnDownPressed;

        public event EventHandler OnSpellPressed;

        public event EventHandler OnPhysicalAttackPressed;

        public KeyboardMouseController(Form form)
        {
            form.KeyDown += FormKeyDown;
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    if (this.OnUpPressed != null)
                    {
                        this.OnUpPressed(this, new EventArgs());
                    }
                    break;
                case Keys.D:
                    if (this.OnRightPressed != null)
                    {
                        this.OnRightPressed(this, new EventArgs());
                    }
                    break;
                case Keys.S:
                    if (this.OnDownPressed != null)
                    {
                        this.OnDownPressed(this, new EventArgs());
                    }
                    break;
                case Keys.A:
                    if (this.OnLeftPressed != null)
                    {
                        this.OnLeftPressed(this, new EventArgs());
                    }
                    break;
                case Keys.O:
                    if (this.OnSpellPressed != null)
                    {
                        this.OnSpellPressed(this, new EventArgs());
                    }
                    break;
                case Keys.P:
                    if (this.OnPhysicalAttackPressed != null)
                    {
                        this.OnPhysicalAttackPressed(this, new EventArgs());
                    }
                    break;
                default:
                    break;
            }
        }
    }

}
