namespace AlduinRPG.Views
{
    using System.Drawing;
    using System.Windows.Forms;
    using System;
    using Models;

    public static class RenderObject
    {
        private const int ImageSize = 70;
        private const int ProgressBarWidth = 100;
        private const int ProgressBarHeight = 20;

        public static void RenderImage(GameForm gameForm, Image image, Coordinates coordinates, Coordinates offset = default(Coordinates))
        {
            try
            {
                var picBox = new PictureBox();
                var imageCoordinates = new Coordinates(coordinates.X, coordinates.Y);
                var imageCoordinatesWithOffset = imageCoordinates * offset;

                picBox.BackgroundImage = image;
                picBox.BackColor = Color.Transparent;
                picBox.Image = image;
                picBox.Parent = gameForm;
                picBox.Location = new Point(imageCoordinatesWithOffset.X, imageCoordinatesWithOffset.Y);
                picBox.Size = new Size(ImageSize, ImageSize);
                gameForm.Controls.Add(picBox);
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("File not found: " + e);
            }
        }

        public static void RenderProgressBar(GameForm gameForm, int maxValue, int currentValue, Coordinates coordinates, Color color)
        {
            var progressBar = new ProgressBar();
            progressBar.Size = new Size(ProgressBarWidth, ProgressBarHeight);
            progressBar.Location = new Point(coordinates.X, coordinates.Y);
            progressBar.Maximum = maxValue;
            progressBar.Value = currentValue;
            progressBar.BackColor = color;
            gameForm.Controls.Add(progressBar);
        }
    }
}
