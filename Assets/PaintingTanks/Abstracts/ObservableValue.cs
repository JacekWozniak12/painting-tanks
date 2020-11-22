namespace PaintingTanks.Abstracts
{
    using System;
    using System.Collections;
    using Interfaces;

    public abstract class ObservableValue<T> : IValueProvider<T>, IValueReceiver<T>
    {
        T _value;

        public T Value
        {
            get => _value;

            set
            {
                _value = value;
                Changed?.Invoke(_value);
            }
        }

        public event Action<T> Changed;
        public void SubscribeToProvider(IValueReceiver<T> receiver)
        {
            Changed += receiver.OnChange;
        }
        public void OnChange(T value) => Value = value;
        public static implicit operator T(ObservableValue<T> ov) => ov._value;
    }

    [Serializable] public class ObservableIntValue : ObservableValue<int>{}
    [Serializable] public class ObservableFloatValue : ObservableValue<float>{}
    [Serializable] public class ObservableStringValue : ObservableValue<string>{}
    [Serializable] public class ObservableCollectionValue : ObservableValue<IEnumerable>{}
}