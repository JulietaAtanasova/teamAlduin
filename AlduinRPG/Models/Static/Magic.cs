namespace AlduinRPG.Models
{
    public class Magic : StaticUnit
    {
        private int damagePower;
        public Magic(Coordinates coordinates) : base(coordinates)
        {
        }

        public int DamagePower { get; set; }
    }
}
