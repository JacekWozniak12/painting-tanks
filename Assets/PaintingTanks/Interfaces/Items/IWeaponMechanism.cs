namespace PaintingTanks.Interfaces
{
    public interface IWeaponMechanism
    {
        bool IsTriggered();
        void Trigger(bool isOn);
        void Ready(bool isTrue);
        void SecondaryButton(bool isTrue);
    }
}