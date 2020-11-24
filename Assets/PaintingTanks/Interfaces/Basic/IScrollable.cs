namespace PaintingTanks.Interfaces
{
    public interface IScrollable
    {
        void Scrolling(float value);
        void ScrollDown();
        void ScrollUp();
    }
}