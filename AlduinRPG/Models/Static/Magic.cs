namespace AlduinRPG.Models
{
    using Interfaces;

    public class Magic : StaticUnit, ITimeoutable
    {
        public Magic(Coordinates coordinates, int damagePower, int maxTimeout) : base(coordinates)
        {
            this.DamagePower = damagePower;
            this.CurrentTimeout = 0;
            this.MaxTimeout = maxTimeout;
        }

        public int DamagePower { get; private set; }

        public int MaxTimeout { get; private set; }

        public int CurrentTimeout { get; private set; }

        public bool HasTimedOut
        {
            get { return this.CurrentTimeout >= this.MaxTimeout; }
        }

        public void IncreaseCurrentTimeout()
        {
            this.CurrentTimeout++;
        }
    }
}
