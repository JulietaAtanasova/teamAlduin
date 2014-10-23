namespace AlduinRPG.Models
{
    ﻿using System.Collections.Generic;

    public class Units
    {
        public Units()
        {
            this.Enemies = new Dictionary<Coordinates, Enemy>();
            this.Obstacles = new Dictionary<Coordinates, Obstacle>();
            this.Chests = new Dictionary<Coordinates, Chest>();
            this.Teleports = new Dictionary<Coordinates, Teleportation>();
            this.Magics = new Dictionary<Coordinates, Magic>();
        }

        public Hero Hero { get; set; }

        public IDictionary<Coordinates, Enemy> Enemies { get; set; }

        public IDictionary<Coordinates, Obstacle> Obstacles { get; set; }

        public IDictionary<Coordinates, Chest> Chests { get; set; }

        public IDictionary<Coordinates, Teleportation> Teleports { get; set; }

        public IDictionary<Coordinates, Magic> Magics { get; set; }

        public bool ContainsUnit(Coordinates coordinates)
        {
            bool hasHero = this.Hero != null;
            return  (hasHero && this.Hero.Coordinates == coordinates)||
                   this.Enemies.ContainsKey(coordinates) ||
                   this.Obstacles.ContainsKey(coordinates) ||
                   this.Chests.ContainsKey(coordinates) ||
                   this.Teleports.ContainsKey(coordinates);
        }
    }
}

