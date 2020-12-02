namespace PaintingTanks.Interfaces
{
    public interface IWeapon
    {
         IReoladable GetMagazine();
         IWeaponMechanism GetMechanism();
    }
}