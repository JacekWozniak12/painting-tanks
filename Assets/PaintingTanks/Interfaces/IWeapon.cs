namespace PaintingTanks.Interfaces
{
    using System.Collections;

    public interface IWeapon
    {
        void TriggerOn();
        void TriggerOff();
        void Ready();
        void Unready();
        bool IsShooting();
        IEnumerator HandleRateOfFire(float time);
    }
}