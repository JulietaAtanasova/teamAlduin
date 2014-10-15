namespace AlduinRPG.Models
{
    public interface IMovable
    {
        Direction Direction { get; set; }
        void Move();
    }
}
