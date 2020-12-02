namespace PaintingTanks.Interfaces
{
    public interface IWeaponMechanism
    {
        bool IsShooting();
        void Trigger(bool isOn);
        void Ready(bool isTrue);
    }
}