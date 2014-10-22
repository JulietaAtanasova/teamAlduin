namespace AlduinRPG.Views
{
    using System.Drawing;
    using System.Windows.Forms;
    using System;
    using Models;

    public static class RenderObject
    {
        private const int ImageSize = 70;

        public static void RenderImage(GameForm gameForm, Image image, Coordinates coordinates)
        {
            try
            {
                var picBox = new PictureBox();
                picBox.BackgroundImage = image;
                picBox.BackColor = Color.Transparent;
                picBox.Image = image;
                picBox.Parent = gameForm;
                picBox.Location = new Point(coordinates.X, coordinates.Y);
                picBox.Size = new Size(ImageSize, ImageSize);
                gameForm.Controls.Add(picBox);
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("File not found: " + e);
            }
        }
    }
}
