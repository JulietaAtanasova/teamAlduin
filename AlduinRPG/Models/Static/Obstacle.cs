namespace AlduinRPG.Models
{
    public class Obstacle : StaticUnit
    {
        private ObstacleType obstacleType;
        public Obstacle(Coordinates coordinates, ObstacleType obstacleType) : base(coordinates)
        {
            this.ObstacleType = obstacleType;
        }
        public ObstacleType ObstacleType { get; set; }
    }
}
