namespace AlduinRPG.Models
{
    public struct Coordinates
    {
        private int x;
        private int y;
        public Coordinates(int x, int y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

    }
}
