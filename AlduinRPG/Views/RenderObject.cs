namespace AlduinRPG.Views
{
    using System.Drawing;
    using System.Windows.Forms;
    using System;

    public static class RenderObject
    {
        public static void RenderImage(GameForm gameForm, Image image, int x, int y, int width = 0, int height = 0)
        {
            try
            {
                var picBox = new PictureBox();
                //picBox.BackColor = Color.Transparent;
                picBox.Image = image;
                picBox.Parent = gameForm;
                picBox.Location = new Point(x, y);
                picBox.Size = new Size(width, height);
                gameForm.Controls.Add(picBox);
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("File not found: " + e);
            }
        }
    }
}
