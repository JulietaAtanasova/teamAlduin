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

        public static bool operator ==(Coordinates first, Coordinates second)
        {
            return first.X == second.X && first.Y == second.Y;
        }
        public static bool operator !=(Coordinates first, Coordinates second)
        {
            return !(first == second);
        }

        public static Coordinates operator-(Coordinates first, Coordinates second)
        {
            int newX = first.X - second.X;
            int newY = first.Y - second.Y;
            return new Coordinates(newX, newY);
        }
        public static Coordinates operator +(Coordinates first, Coordinates second)
        {
            int newX = first.X + second.X;
            int newY = first.Y + second.Y;
            return new Coordinates(newX, newY);
        }
        public static Coordinates operator *(Coordinates first, Coordinates second)
        {
            int newX = first.X * second.X;
            int newY = first.Y * second.Y;
            return new Coordinates(newX, newY);
        }
    }
}
