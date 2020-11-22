namespace PaintingTanks.Interfaces
{
    public interface IValueReceiver<T>
    {
        void OnChange(T value);
    }

    public interface IValueReceiver<T0, T1>
    {
        void OnChange(T0 value, T1 value1);
    }

    public interface IValueReceiver<T0, T1, T2>
    {
        void OnChange(T0 value, T1 value1, T2 value2);
    }

}