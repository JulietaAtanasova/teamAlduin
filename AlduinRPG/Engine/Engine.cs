namespace AlduinRPG.Engine
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using Models;
    using System.Threading;
    using Views;

    public class Engine
    {
        private readonly int ObstacleCount;
        private readonly int EnemyCount;
        private readonly GameMap gameMap;
        private readonly Random random = new Random();
        private Units units;
        private GameForm gameForm;
        private RendererView renderer;

        public Engine(GameForm gameForm, GameMap gameMap)
        {
            this.gameMap = gameMap;
            this.ObstacleCount = gameMap.Width;
            this.EnemyCount = gameMap.Height;
            this.units = new Units();
            this.gameForm = gameForm;
            this.renderer = new RendererView(this.gameForm);
            this.Initialize();
        }

        public void Run()
        {
            this.ResurrectDeadEnemies();
            this.units.Hero.Recover();
            this.MoveEnemies();
            this.ProcessCollisionsEnemyHero();
            if (this.GameOver())
            {
                // TODO - render gameover
            }
            renderer.Render(units, gameMap);
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
            for (int i = 0; i < this.gameMap.Width; i++)
            {
                this.AddRandomObstacle(new Coordinates(i, 0));
                this.AddRandomObstacle(new Coordinates(i, this.gameMap.Height - 1));
            }

            for (int j = 1; j < this.gameMap.Height - 1; j++)
            {
                this.AddRandomObstacle(new Coordinates(0, j));
                this.AddRandomObstacle(new Coordinates(this.gameMap.Width - 1, j));
            }
        }

        private void AddHero(HeroType heroType)
        {
            Coordinates coordinates = this.GetRandomCoordinates();
            switch (heroType)
            {
                case HeroType.Warrior:
                    this.units.Hero = new Warrior(coordinates);
                    break;
                case HeroType.FemaleWarrior:
                    this.units.Hero = new FemaleWarrior(coordinates);
                    break;
                case HeroType.Magician:
                    this.units.Hero = new Magician(coordinates);
                    break;
                default:
                    throw new NotImplementedException("This hero type was not implemented yet.");
            }
        }

        private void AddObstacles()
        {
            for (int i = 0; i < this.ObstacleCount; i++)
            {
                this.AddRandomObstacle(this.GetRandomCoordinates());
            }
        }

        private void AddEnemies()
        {
            for (int i = 0; i < this.EnemyCount; i++)
            {
                this.AddRandomEnemy();
            }
        }

        private void AddRandomObstacle(Coordinates coordinates)
        {
            ObstacleType obstacleType = (ObstacleType)this.random.Next(0, 3);
            this.units.Obstacles.Add(coordinates, new Obstacle(coordinates, obstacleType));
        }

        private void AddRandomEnemy()
        {
            Coordinates coordinates = this.GetRandomCoordinates();
            EnemyType enemyType = (this.units.Hero.Level > 1) ? EnemyType.BossEnemy : EnemyType.WeakEnemy; // ??? 
            switch (enemyType)
            {
                case EnemyType.WeakEnemy:
                    this.units.Enemies.Add(coordinates, new WeakEnemy(coordinates));
                    break;
                case EnemyType.BossEnemy:
                    this.units.Enemies.Add(coordinates, new BossEnemy(coordinates));
                    break;
                default:
                    throw new NotImplementedException("This enemy type was not implemented yet.");
            }
        }

        private void ResurrectDeadEnemies()
        {
            Enemy[] enemyValues = new Enemy[this.units.Enemies.Values.Count];
            this.units.Enemies.Values.CopyTo(enemyValues, 0);
            for (int i = 0; i < enemyValues.Length; i++)
            {
                Enemy enemy = enemyValues[i];
                if (enemy.IsAlive == false)
                {
                    Coordinates oldCoordinates = enemy.Coordinates;
                    enemy.Resurrect(GetRandomCoordinates());
                    Coordinates newCoordinates = enemy.Coordinates;
                    this.units.Enemies.Remove(oldCoordinates);
                    this.units.Enemies.Add(newCoordinates, enemy);
                }
            }
        }

        private void MoveEnemies()
        {
            Enemy[] enemyValues = new Enemy[this.units.Enemies.Values.Count];
            this.units.Enemies.Values.CopyTo(enemyValues, 0);
            for (int i = 0; i < enemyValues.Length; i++)
            {
                Enemy enemy = enemyValues[i];
                Coordinates oldCoordinates = enemy.Coordinates;
                Direction direction = this.GetDirection(oldCoordinates);
                enemy.Move(direction);
                Coordinates newCoordinates = enemy.Coordinates;
                this.units.Enemies.Remove(oldCoordinates);
                this.units.Enemies.Add(newCoordinates, enemy);
            }
        }

        private void ProcessCollisionsEnemyHero()
        {
            var enemyInRange = FindEnemyInRange();
            if (enemyInRange == null)
            {
                return;
            }

            EnemyAttack(enemyInRange);
        }

        private Enemy FindEnemyInRange(int range = 1)
        {
            int heroX = this.units.Hero.Coordinates.X;
            int heroY = this.units.Hero.Coordinates.Y;
            for (int i = heroX - range; i <= heroX + range; i++)
            {
                for (int j = heroY - range; j <= heroY + range; j++)
                {
                    Coordinates coordinates = new Coordinates(i, j);
                    if (this.units.Enemies.ContainsKey(coordinates))
                    {
                        return this.units.Enemies[coordinates];
                    }
                }
            }

            return null;
        }

        private void EnemyAttack(Enemy enemy)
        {
            this.units.Hero.TakeDamage(enemy.PhysicalAttack());
        }

        private bool GameOver()
        {
            if (this.units.Hero.CurrentLives == 0 && !this.units.Hero.IsAlive)
            {
                return true;
            }

            return false;
        }

        private Coordinates GetRandomCoordinates()
        {
            // TODO counter for exception
            int x = this.random.Next(0, this.gameMap.Width);
            int y = this.random.Next(0, this.gameMap.Height);
            Coordinates coordinates = new Coordinates(x, y);
            if (!this.units.ContainsUnit(coordinates))
            {
                return coordinates;
            }

            return this.GetRandomCoordinates();
        }

        private Direction GetDirection(Coordinates currentCoordinates)
        {
            Direction direction = (Direction)this.random.Next(0, 4);
            return CanMove(currentCoordinates, direction) ? direction : this.GetDirection(currentCoordinates);
        }

        public void SubscribeToUserInput(IUserInput userInterface)
        {
            userInterface.OnUpPressed += (sender, args) =>
            {
                this.TryMoveHero(Direction.Up);
            };
            userInterface.OnDownPressed += (sender, args) =>
            {
                this.TryMoveHero(Direction.Down);
            };
            userInterface.OnRightPressed += (sender, args) =>
            {
                this.TryMoveHero(Direction.Right);
            };
            userInterface.OnLeftPressed += (sender, args) =>
            {
                this.TryMoveHero(Direction.Left);
            };
            userInterface.OnPhysicalAttackPressed += (sender, args) =>
            {
                this.HeroAttack();
            };
            userInterface.OnSpellPressed += (sender, args) =>
            {
                this.HeroMagicAttack();
            };
        }

        private void HeroMagicAttack()
        {
            var magics = this.units.Hero.CastMagic();
            foreach (var magic in magics)
            {
                if (units.Enemies.ContainsKey(magic.Coordinates))
                {
                    Enemy enemy = units.Enemies[magic.Coordinates];
                    enemy.TakeDamage(magic.DamagePower);
                }
            }
        }

        private void HeroAttack()
        {
            var enemyInRange = this.FindEnemyInRange();
            if (enemyInRange == null)
            {
                return;
            }

            enemyInRange.TakeDamage(this.units.Hero.PhysicalAttack());
        }

        private void TryMoveHero(Direction direction)
        {
            if (CanMove(this.units.Hero.Coordinates, direction))
            {
                this.units.Hero.Move(direction);
            }
        }

        private bool CanMove(Coordinates currentCoordinates, Direction direction)
        {
            int nextX = currentCoordinates.X;
            int nextY = currentCoordinates.Y;
            switch (direction)
            {
                case Direction.Up:
                    nextY--;
                    break;
                case Direction.Right:
                    nextX++;
                    break;
                case Direction.Down:
                    nextY++;
                    break;
                case Direction.Left:
                    nextX--;
                    break;
            }

            Coordinates nextCoordinates = new Coordinates(nextX, nextY);
            bool canMove = !this.units.ContainsUnit(nextCoordinates);
            return canMove;
        }
    }
}
