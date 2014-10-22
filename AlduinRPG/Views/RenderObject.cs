namespace AlduinRPG.Views
{
    using System.Drawing;
    using System.Windows.Forms;
    using System;
    using Models;

    public static class RenderObject
    {
        public static void RenderImage(GameForm gameForm, Image image, Coordinates coordinates, int width = 70, int height = 70)
        {
            try
            {
                var picBox = new PictureBox();
                picBox.BackColor = Color.Transparent;
                picBox.Image = image;
                picBox.Parent = gameForm;
                picBox.Location = new Point(coordinates.X, coordinates.Y);
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
