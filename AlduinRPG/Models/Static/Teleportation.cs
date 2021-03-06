﻿namespace AlduinRPG.Models
{
    using System;

    public class Teleportation : StaticUnit
    {
        public Teleportation(Coordinates coordinates) : base(coordinates)
        {
        }

        public Coordinates Teleport(GameMap gameMap)
        {
            Random random = new Random();
            int x = random.Next(1, gameMap.Width);
            int y = random.Next(1, gameMap.Height);
            Coordinates teleportCoordinates = new Coordinates(x, y);
            return teleportCoordinates;
        }
    }
}
