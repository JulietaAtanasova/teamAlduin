namespace AlduinRPG.Models
{
    public abstract class Unit
    {
        protected Unit(Coordinates coordinates)
        {
            this.Coordinates = coordinates;
        }

        public Coordinates Coordinates { get; set; } 
    }
}
