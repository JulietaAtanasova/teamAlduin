namespace AlduinRPG.Models
{
    ﻿using System.Collections.Generic;

    public class Units
    {
        private Hero hero;
        private IDictionary<Coordinates, Enemy> enemies;
        private IDictionary<Coordinates, Obstacle> obstacles;
        private IDictionary<Coordinates, Chest> chests;
        private IDictionary<Coordinates, Teleportation> teleports;
        private IDictionary<Coordinates, Magic> magics; 

        public Units()
        {
            this.enemies = new Dictionary<Coordinates, Enemy>();
            this.obstacles = new Dictionary<Coordinates, Obstacle>();
            this.chests = new Dictionary<Coordinates, Chest>();
            this.teleports = new Dictionary<Coordinates, Teleportation>();
            this.magics = new Dictionary<Coordinates, Magic>();
        }

        public Hero Hero
        {
            get { return this.hero; }
            set { this.hero = value; }
        }

        public IDictionary<Coordinates, Enemy> Enemies
        {
            get { return this.enemies; }
            set { this.enemies = value; }
        }

        public IDictionary<Coordinates, Obstacle> Obstacles
        {
            get { return this.obstacles; }
            set { this.obstacles = value; }
        }

        public IDictionary<Coordinates, Chest> Chests
        {
            get { return this.chests; }
            set { this.chests = value; }
        }

        public IDictionary<Coordinates, Teleportation> Teleports
        {
            get { return this.teleports; }
            set { this.teleports = value; }
        }

        public IDictionary<Coordinates, Magic> Magics
        {
            get { return this.magics; }
            set { this.magics = value; }
        } 

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

