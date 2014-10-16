namespace AlduinRPG.Interfaces
{
    using System;

    public interface IUserInputInterface
    {
        event EventHandler OnRightPressed;
        event EventHandler OnLeftPressed;
        event EventHandler OnUpPressed;
        event EventHandler OnDownPressed;
        event EventHandler OnSpellPressed;
        event EventHandler OnPhysicalAttackPressed;
    }
}
