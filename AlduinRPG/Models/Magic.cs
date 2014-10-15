namespace AlduinRPG.Models
{
    public class Magic : StaticUnit
    {
        public Magic(Coordinates coordinates) : base(coordinates)
        {
        }

        public int DamagePower { get; set; }
    }
}
