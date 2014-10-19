using AlduinRPG.Interfaces;

namespace AlduinRPG.Models
{
    public abstract class Unit : IUnit
    {
        private Coordinates coordinates;

        protected Unit(Coordinates coordinates)
        {
            this.Coordinates = coordinates;
        }

        public Coordinates Coordinates { get; set; } 
    }
}
