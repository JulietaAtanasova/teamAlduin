namespace AlduinRPG.Interfaces
{
    public interface ILiving
    {
        void PhysicallAttack();
        
        int TakeDamage(int attackPoints);
                
        void Resurrect();

    }
}
