using System;
using System.Collections.Generic;
using AlduinRPG.Interfaces;
using AlduinRPG.Models;

namespace AlduinRPG.Engine
{
    public class Engine
    {
        private GameMap gameMap;
        private Dictionary<Coordinates, IUnit> units;
        private Random random = new Random();

        public Engine()
        {
            
        }

        private void Initialize()
        {
            this.CreateBorder();
            this.AddHero();
        }

        private void CreateBorder()
        {
            for (int i = 0; i < gameMap.Width; i++)
            {
                AddRandomObstacle(new Coordinates(i, 0));
                AddRandomObstacle(new Coordinates(i, gameMap.Height - 1));
            }
            for (int j = 1; j < gameMap.Height - 1; j++)
            {
                AddRandomObstacle(new Coordinates(0, j));
                AddRandomObstacle(new Coordinates(gameMap.Width - 1, j));
            }
        }

        private void AddRandomObstacle(Coordinates coordinates)
        {
            ObstacleType obstacleType = (ObstacleType)random.Next(0, 3);
            units.Add(coordinates, new Obstacle(coordinates, obstacleType));
        }

        private void AddHero(HeroType heroType)
        {
            switch (heroType)
            {
                case HeroType.Warrior:
                    // TODO
                    break;
                case HeroType.Magician:
                    // TODO
                    break;
                default: 
                    throw new NotImplementedException("This hero type was not implemented yet.");
            }
        }
    }
}
