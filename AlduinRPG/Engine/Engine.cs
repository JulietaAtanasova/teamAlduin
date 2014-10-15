using System;
using System.Collections.Generic;
using AlduinRPG.Interfaces;
using AlduinRPG.Models;

namespace AlduinRPG.Engine
{
    public class Engine
    {
        private int ObstacleCount;
        private int EnemyCount;

        private GameMap gameMap;
        private Dictionary<Coordinates, IUnit> units;
        private Random random = new Random();
        
        public Engine(GameMap gameMap)
        {
            this.gameMap = gameMap;
            this.ObstacleCount = gameMap.Width;
            this.EnemyCount = gameMap.Height;
        }

        private void Initialize()
        {
            this.CreateBorder();
            this.AddHero(HeroType.Warrior);
            this.AddObstacles();
            this.AddEnemies();
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

        private Coordinates GetRandomCoordinates()
        {
            int x = random.Next(0, gameMap.Width);
            int y = random.Next(0, gameMap.Height);
            Coordinates coordinates = new Coordinates(x,y);
            if (!units.ContainsKey(coordinates))
            {
                return coordinates;
            }
            return GetRandomCoordinates();
        }

        private void AddObstacles()
        {
            for (int i = 0; i < this.ObstacleCount; i++)
            {
                AddRandomObstacle(GetRandomCoordinates());
            }
        }

        private void AddEnemies()
        {
            for (int i = 0; i < this.EnemyCount; i++)
            {
                Coordinates coordinates = GetRandomCoordinates();
                EnemyType enemyType = (EnemyType) random.Next(0, 2);
                switch (enemyType)
                {
                    case EnemyType.WeakEnemy:
                        // TODO
                        break;
                    case EnemyType.BossEnemy:
                        // TODO
                        break;
                    default:
                        throw new NotImplementedException("This enemy type was not implemented yet.");
                }
            }
        }
    }
}
