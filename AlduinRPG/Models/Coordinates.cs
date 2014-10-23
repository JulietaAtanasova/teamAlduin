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

        public int X
        {
            get
            {
                return this.x;
            }

            set
            {
                if (value < 0)
                {
                    // throw new ArgumentOutOfRangeException("Coordinates","Coordinates must be positive");
                }

                this.x = value;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }

            set
            {
                if (value < 0)
                {
                    // throw new ArgumentOutOfRangeException("Coordinates", "Coordinates must be positive");
                }

                this.y = value;
            }
        }

        public static bool operator ==(Coordinates first, Coordinates second)
        {
            return first.X == second.X && first.Y == second.Y;
        }

        public static bool operator !=(Coordinates first, Coordinates second)
        {
            return !(first == second);
        }

        public static Coordinates operator -(Coordinates first, Coordinates second)
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
