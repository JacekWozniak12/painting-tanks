namespace PaintingTanks.Interfaces
{
    using PaintingTanks.Definitions.ScriptableObjects;

    public interface IReoladable
    {
        int Reload(int amount);
        AmmoType GetAmmoType();
    }
}