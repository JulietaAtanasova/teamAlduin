namespace AlduinRPG.Interfaces
{
    public interface ITimeoutable
    {
        int MaxTimeout { get; }

        int CurrentTimeout { get; }

        bool HasTimedOut { get; }
    }
}
