namespace PaintingTanks.Interfaces
{
    public interface IWeapon
    {
        bool IsShooting();
        void Trigger(bool isOn);
        void Ready(bool isTrue);
    }
}