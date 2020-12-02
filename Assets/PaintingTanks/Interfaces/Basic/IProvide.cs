namespace PaintingTanks.Interfaces
{
    public interface IProvide<T>
    {
        T getProvided(object[] args);
    }
}