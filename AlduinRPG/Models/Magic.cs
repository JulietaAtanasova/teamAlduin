namespace AlduinRPG.Models
{
    public class Magic : StaticUnit
    {
        public Magic(Coordinates coordinates, int range) : base(coordinates)
        {
            this.Range = range;
        }

        public int Range { get; set; }
        public int DamagePower { get; set; }
    }
}
