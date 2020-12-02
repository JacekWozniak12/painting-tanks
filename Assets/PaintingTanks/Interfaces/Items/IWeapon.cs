using PaintingTanks.Actor.Control;

namespace PaintingTanks.Interfaces
{
    public interface IWeapon
    {
        IReoladable GetMagazine();
        IWeaponMechanism GetMechanism();
        float GetMinimalRange();
        float GetMaximalRange();
        bool GetAngleConstraint(out float Angle);
        bool IsFiring();
        MovementScheme GetMovementScheme();
    }
}