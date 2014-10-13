namespace AlduinRPG.Models
{
    public class Chest : Bonus
    {
        public Chest(Coordinates coordinates) : base(coordinates)
        {
        }

        public string Name { get; set; }
    }
}
