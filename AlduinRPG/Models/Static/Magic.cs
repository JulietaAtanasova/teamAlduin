namespace AlduinRPG.Models
{
    public class Magic : StaticUnit
    {
        private int damagePower;
        public Magic(Coordinates coordinates, int damagePower) : base(coordinates)
        {
            this.DamagePower = damagePower;
        }

        public int DamagePower { get; set; }
    }
}
