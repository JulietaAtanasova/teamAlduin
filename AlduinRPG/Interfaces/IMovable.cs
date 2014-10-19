namespace AlduinRPG.Models
{
    public interface IMovable
    {
        Direction Direction { get; set; }
        Coordinates Move(Direction direction);
    }
}
