using AlduinRPG.Models;

namespace AlduinRPG.Interfaces
{
    public interface ILiving
    {
        int PhysicallAttack();
        
        void TakeDamage(int attack);
                
        Coordinates Resurrect(GameMap gameMap);

    }
}
