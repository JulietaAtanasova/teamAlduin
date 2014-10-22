namespace AlduinRPG.Models
{
    ﻿using System.Collections.Generic;

    public class Units
    {
        private Hero hero;
        private Dictionary<Coordinates, Enemy> enemies;
        private Dictionary<Coordinates, Obstacle> obstacles;
        private Dictionary<Coordinates, Chest> chests;
        private Dictionary<Coordinates, Teleportation> teleports;

        public Units()
        {
            this.enemies = new Dictionary<Coordinates, Enemy>();
            this.obstacles = new Dictionary<Coordinates, Obstacle>();
            this.chests = new Dictionary<Coordinates, Chest>();
            this.teleports = new Dictionary<Coordinates, Teleportation>();
        }

        public Hero Hero
        {
            get { return this.hero; }
            set { this.hero = value; }
        }
        public Dictionary<Coordinates, Enemy> Enemies
        {
            get { return this.enemies; }
            set { this.enemies = value; }
        }
        public Dictionary<Coordinates, Obstacle> Obstacles
        {
            get { return this.obstacles; }
            set { this.obstacles = value; }
        }
        public Dictionary<Coordinates, Chest> Chests
        {
            get { return this.chests; }
            set { this.chests = value; }
        }
        public Dictionary<Coordinates, Teleportation> Teleports
        {
            get { return this.teleports; }
            set { this.teleports = value; }
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

