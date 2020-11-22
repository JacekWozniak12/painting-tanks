namespace PaintingTanks.Interfaces
{
    public interface IValueProvider<T>
    {
        void SubscribeToProvider(IValueReceiver<T> receiver);
    }
    public interface IValueProvider<T0, T1>
    {
        void SubscribeToProvider(IValueReceiver<T0, T1> receiver);
    }

    public interface IValueProvider<T0, T1, T2>
    {
        void SubscribeToProvider(IValueReceiver<T0, T1, T2> receiver);
    }
}