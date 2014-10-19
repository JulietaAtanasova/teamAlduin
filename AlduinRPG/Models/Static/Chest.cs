namespace AlduinRPG.Models
{
    public class Chest : Bonus
    {
        private ChestType chestType;
        public Chest(Coordinates coordinates, ChestType chestType) : base(coordinates)
        {
            this.ChestType = chestType;
        }

        public ChestType ChestType { get; set; }
    }
}
