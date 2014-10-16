namespace AlduinRPG.Models
{
    public class GameMap
    {
        private readonly int widthRatio;
        private readonly int heightRatio;

        public GameMap(MapType mapType, int widthRatio = 4, int heightRatio = 3)
        {
            this.widthRatio = widthRatio;
            this.heightRatio = heightRatio;
            this.Width = (int)mapType * this.widthRatio;
            this.Height = (int)mapType * this.heightRatio;
        }

        public int Width { get; set; }
        public int Height { get; set; }

    }
}
