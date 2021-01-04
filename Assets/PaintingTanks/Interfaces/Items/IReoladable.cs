using PaintingTanks.Definitions.ScriptableObjects;

namespace PaintingTanks.Interfaces
{
    public interface IReoladable
    {
        int Reload(int amount);
        AmmoType GetAmmoType();
    }
}