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
        private const int RefreshRate = 500;
        private readonly int ObstacleCount;
        private readonly int EnemyCount;
        private readonly GameMap gameMap;
        private readonly Random random = new Random();
        private Dictionary<Coordinates, IUnit> OLD_UNITS;
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
        }

        public void Run()
        {
            this.Initialize();
            while (true)
            {
                this.units.Hero.Recover();
                this.MoveEnemies();
                this.ProcessCollisions();
                if (this.GameOver())
                {
                    break;
                }
                renderer.Render(units, gameMap);
                Thread.Sleep(Engine.RefreshRate);
            }

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

        private void MoveEnemies()
        {
            foreach (var enemy in this.units.Enemies.Values)
            {
                    Direction direction = this.GetDirection(enemy.Coordinates);
                    enemy.Move(direction);
            }
        }

        private void ProcessCollisions()
        {
            ProcessCollisionsEnemyHero();
            // TODO Enemy/Magic - > in HeroMagicAttack
            this.ResurrectDeadEnemies();

        }

        private void ResurrectDeadEnemies()
        {
            var enemies = GetEnemies();

            foreach (var enemy in enemies)
            {
                if (enemy.IsAlive == false)
                {
                    enemy.Resurrect(GetRandomCoordinates());
                }
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

        private Enemy FindEnemyInRange()
        {
            var enemies = GetEnemies();
            var hero = GetHero();
            foreach (var enemy in enemies)
            {
                bool checkX = hero.Coordinates.X >= enemy.Coordinates.X - 1
                              && hero.Coordinates.X <= enemy.Coordinates.X + 1;
                bool checkY = hero.Coordinates.Y >= enemy.Coordinates.Y - 1
                              && hero.Coordinates.Y <= enemy.Coordinates.Y + 1;
                if (checkX && checkY)
                {
                    return enemy;
                }
            }

            return null;
        }

        private void EnemyAttack(Enemy enemy)
        {
            GetHero().TakeDamage(enemy.PhysicalAttack());
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

        private Hero GetHero()
        {
            Hero hero = null; // ???????????
            foreach (var unit in this.OLD_UNITS)
            {
                if (unit.Value is Hero)
                {
                    hero = unit.Value as Hero;
                    break;
                }
            }

            if (hero == null)
            {
                throw new ArgumentNullException("hero", "Cannot find hero.");
            }

            return hero;
        }

        private IEnumerable<Enemy> GetEnemies()
        {
            var enemies = new List<Enemy>();
            foreach (var unit in this.OLD_UNITS)
            {
                if (unit.Value is Enemy)
                {
                    enemies.Add(unit.Value as Enemy);
                }
            }

            if (enemies == null)
            {
                throw new ArgumentNullException("enemies", "Cannot find enemies.");
            }

            return enemies;
        }

        private Direction GetDirection(Coordinates currentCoordinates)
        {
            Direction direction = (Direction)this.random.Next(0, 4);
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
            if (!this.units.ContainsUnit(nextCoordinates))
            {
                return direction;
            }

            return this.GetDirection(currentCoordinates);
        }

        public void SubscribeToUserInput(IUserInput userInterface)
        {
            userInterface.OnUpPressed += (sender, args) =>
            {
                this.MovePlayerUp();
            };
            userInterface.OnDownPressed += (sender, args) =>
            {
                this.MovePlayerDown();
            };
            userInterface.OnRightPressed += (sender, args) =>
            {
                this.MovePlayerRight();
            };
            userInterface.OnLeftPressed += (sender, args) =>
            {
                this.MovePlayerLeft();
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
            var hero = GetHero();
            var magics = hero.CastMagic();

            foreach (var magic in magics)
            {
                if (OLD_UNITS.ContainsKey(magic.Coordinates))
                {
                    if (OLD_UNITS[magic.Coordinates] is Enemy)
                    {
                        Enemy enemy = OLD_UNITS[magic.Coordinates] as Enemy;
                        enemy.TakeDamage(magic.DamagePower);
                    }
                }
            }
        }

        private void HeroAttack()
        {
            Hero hero = GetHero();
            var enemyInRange = this.FindEnemyInRange();
            if (enemyInRange == null)
            {
                return;
            }

            enemyInRange.TakeDamage(hero.PhysicalAttack());
        }

        private void MovePlayerUp()
        {
            this.GetHero().Move(Direction.Up);
        }

        private void MovePlayerDown()
        {
            this.GetHero().Move(Direction.Down);
        }

        private void MovePlayerRight()
        {
            this.GetHero().Move(Direction.Right);
        }

        private void MovePlayerLeft()
        {
            this.GetHero().Move(Direction.Left);
        }
    }
}
