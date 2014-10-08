using System;

namespace AlduinRPG.Models
{
    public class Coordinates
    {
        public Coordinates(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

    }
}
