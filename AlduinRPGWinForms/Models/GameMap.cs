namespace AlduinRPG.Models
{
    public class GameMap
    {
        private const int widthRatio = 4;
        private const int heightRatio = 3;

        private Coordinates coordinates = new Coordinates(0, 0);

        public GameMap(Coordinates coordinates, MapType mapType)
        {
            this.Width = (int)mapType * widthRatio;
            Height = (int)mapType * heightRatio;
            this.coordinates = coordinates;
        }

        public int Width { get; set; }
        public int Height { get; set; }

    }
}
