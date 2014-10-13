using AlduinRPG.Models;

namespace AlduinRPG.Interfaces
{
    public interface ILiving
    {
        void PhysicallAttack();
        
        void TakeDamage(int attack);
                
        Coordinates Resurrect(GameMap gameMap);

    }
}
