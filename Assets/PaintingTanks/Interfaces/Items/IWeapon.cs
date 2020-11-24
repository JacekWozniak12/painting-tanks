namespace PaintingTanks.Interfaces
{

    public interface IWeapon
    {
        void TriggerOn();
        void TriggerOff();
        void Ready();
        void Unready();
        bool IsShooting();
    }
}