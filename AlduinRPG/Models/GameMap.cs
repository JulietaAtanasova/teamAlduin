namespace AlduinRPG.Models
{
    public class GameMap
    {
        private const int widthRatio = 4;
        private const int heightRatio = 3;

        public GameMap(MapType mapType)
        {
            this.Width = (int)mapType * widthRatio;
            Height = (int)mapType * heightRatio;
        }

        public int Width { get; set; }
        public int Height { get; set; }

    }
}
