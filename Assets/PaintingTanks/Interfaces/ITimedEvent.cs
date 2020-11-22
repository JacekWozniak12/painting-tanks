namespace PaintingTanks.Interfaces
{
    public interface ITimedEvent
    {
        float GetTime();
        void Invoke();
    }
}