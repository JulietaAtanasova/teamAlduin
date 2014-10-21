using AlduinRPG.Models;

namespace AlduinRPG.Interfaces
{
    public interface ILiving
    {
        int PhysicalAttack();
        
        void TakeDamage(int attack);
                
        void Resurrect(Coordinates coordinates);

    }
}
