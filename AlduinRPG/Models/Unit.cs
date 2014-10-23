namespace AlduinRPG.Models
{
    using Interfaces;

    public abstract class Unit : IUnit
    {
        protected Unit(Coordinates coordinates)
        {
            this.Coordinates = coordinates;
        }

        public Coordinates Coordinates { get; set; } 
    }
}
