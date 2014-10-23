namespace AlduinRPG.Engine
{
    using System;
    using Interfaces;
    using Models;
    using Views;

    public class Engine
    {
        private const int DefaultChestCount = 2;
        private const int DefaultTeleportCount = 1;
        private readonly int obstacleCount;
        private readonly int enemyCount;
        private readonly int chestCount;
        private readonly int teleportCount;
        private readonly GameMap gameMap;
        private readonly Random random = new Random();
        private readonly Units units = new Units();
        private readonly RendererView renderer;
        private GameForm gameForm;

        public Engine(GameForm gameForm, GameMap gameMap, IUserInput controller, HeroType hero)
        {
            this.SubscribeToUserInput(controller);
            this.gameMap = gameMap;
            this.gameForm = gameForm;

            this.renderer = new RendererView(this.gameForm);
            this.obstacleCount = this.gameMap.Width;
            this.enemyCount = this.gameMap.Height;
            this.chestCount = Engine.DefaultChestCount;
            this.teleportCount = Engine.DefaultTeleportCount;

            this.Initialize(hero);
        }

        public void Run()
        {
            this.ProcessMagicsTimeout();
            this.MoveEnemies();
            this.ProcessCollisionsEnemyHero();
            this.TryResurrectHero();
            if (this.GameOver())
            {
                this.renderer.RenderGameOverScreen();
            }

            this.units.Hero.Recover();
            this.ResurrectDeadEnemies();
            this.renderer.Render(this.units, this.gameMap);
        }

        private void SubscribeToUserInput(IUserInput userInterface)
        {
            userInterface.OnUpPressed += (sender, args) => this.TryMoveHero(Direction.Up);
            userInterface.OnDownPressed += (sender, args) => this.TryMoveHero(Direction.Down);
            userInterface.OnLeftPressed += (sender, args) => this.TryMoveHero(Direction.Left);
            userInterface.OnRightPressed += (sender, args) => this.TryMoveHero(Direction.Right);
            userInterface.OnPhysicalAttackPressed += (sender, args) => this.HeroAttack();
            userInterface.OnSpellPressed += (sender, args) => this.HeroMagicAttack();
        }

        private void Initialize(HeroType heroType)
        {
            this.CreateBorder();
            this.AddHero(heroType);
            this.AddUnits(this.obstacleCount, this.AddRandomObstacle);
            this.AddUnits(this.enemyCount, this.AddEnemy);
            this.AddUnits(this.chestCount, this.AddRandomChest);
            this.AddUnits(this.teleportCount, this.AddTeleport);
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

        private void AddUnits(int counter, Action<Coordinates> addUnit)
        {
            for (int i = 0; i < counter; i++)
            {
                addUnit(this.GetRandomCoordinates());
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

        private void AddEnemy(Coordinates coordinates)
        {
            EnemyType enemyType = (this.units.Hero.Level > 1) ? EnemyType.BossEnemy : EnemyType.WeakEnemy;
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

        private void AddRandomObstacle(Coordinates coordinates)
        {
            var obstacleType = (ObstacleType)this.random.Next(0, 3);
            this.units.Obstacles.Add(coordinates, new Obstacle(coordinates, obstacleType));
        }
        
        private void AddRandomChest(Coordinates coordinates)
        {
            var chestType = (ChestType)this.random.Next(0, 3);
            this.units.Chests.Add(coordinates, new Chest(coordinates, chestType));
        }

        private void AddTeleport(Coordinates coordinates)
        {
            this.units.Teleports.Add(coordinates, new Teleportation(coordinates));
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
                    this.units.Enemies.Remove(enemy.Coordinates);
                    this.AddEnemy(this.GetRandomCoordinates());
                }
            }
        }

        private void TryResurrectHero()
        {
            if (!this.units.Hero.IsAlive && this.units.Hero.CurrentLives > 0)
            {
                this.units.Hero.Resurrect(this.GetRandomCoordinates());
            }
        }

        private void ProcessMagicsTimeout()
        {
            var magicValues = new Magic[this.units.Magics.Values.Count];
            this.units.Magics.Values.CopyTo(magicValues, 0);
            foreach (var magic in magicValues)
            {
                if (magic.HasTimedOut)
                {
                    this.units.Magics.Remove(magic.Coordinates);
                }
                else
                {
                    magic.IncreaseCurrentTimeout();
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
            var enemyInRange = this.FindEnemyInRange();
            if (enemyInRange == null)
            {
                return;
            }

            this.EnemyAttack(enemyInRange);
        }

        private void EnemyAttack(Enemy enemy)
        {
            this.units.Hero.TakeDamage(enemy.PhysicalAttack());
        }

        private void HeroMagicAttack()
        {
            this.units.Magics = this.units.Hero.CastMagic();
            foreach (var magic in this.units.Magics.Values)
            {
                if (this.units.Enemies.ContainsKey(magic.Coordinates))
                {
                    Enemy enemy = this.units.Enemies[magic.Coordinates];
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
            if (this.HasChest(this.units.Hero.Coordinates, direction))
            {
                this.units.Hero.Move(direction);
                this.units.Hero.TakeChest(this.units.Chests[this.units.Hero.Coordinates]);
                this.units.Chests.Remove(this.units.Hero.Coordinates);
            }
            else if (this.HasTeleport(this.units.Hero.Coordinates, direction))
            {
                this.units.Hero.Move(direction);
                this.units.Hero.Teleport(this.units.Hero.Coordinates);
                this.units.Teleports.Remove(this.units.Hero.Coordinates);
                this.AddTeleport(this.GetRandomCoordinates());
            }
            else if (this.CanMove(this.units.Hero.Coordinates, direction))
            {
                this.units.Hero.Move(direction);
            }
        }

        private bool GameOver()
        {
            return this.units.Hero.CurrentLives == 0 && !this.units.Hero.IsAlive;
        }

        private bool HasChest(Coordinates currentCoordinates, Direction direction)
        {
            Coordinates nextCoordinates = NextCoordinates(currentCoordinates, direction);
            bool hasChest = this.units.Chests.ContainsKey(nextCoordinates);
            return hasChest;
        }

        private bool HasTeleport(Coordinates currentCoordinates, Direction direction)
        {
            Coordinates nextCoordinates = NextCoordinates(currentCoordinates, direction);
            bool hasTeleport = this.units.Teleports.ContainsKey(nextCoordinates);
            return hasTeleport;
        }

        private bool CanMove(Coordinates currentCoordinates, Direction direction)
        {
            Coordinates nextCoordinates = NextCoordinates(currentCoordinates, direction);
            bool canMove = !this.units.ContainsUnit(nextCoordinates);
            return canMove;
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

        private Coordinates NextCoordinates(Coordinates currentCoordinates, Direction direction)
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

            return new Coordinates(nextX, nextY);
        }

        private Direction GetDirection(Coordinates currentCoordinates)
        {
            Direction direction = (Direction)this.random.Next(0, 4);
            return this.CanMove(currentCoordinates, direction) ? direction : this.GetDirection(currentCoordinates);
        }
    }
}
