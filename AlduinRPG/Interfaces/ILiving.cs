namespace AlduinRPG.Interfaces
{
    using Models;

    public interface ILiving
    {
        int PhysicalAttack();
        
        void TakeDamage(int attack);
                
        void Resurrect(Coordinates coordinates);
    }
}
