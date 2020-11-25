namespace PaintingTanks.Definitions
{
    using UnityEngine;
    using System;
    using System.Collections;
    using Interfaces;

    [Serializable]
    public class ObservableValue<T> : IValueProvider<T>, IValueReceiver<T>
    {
        [SerializeField] T _value;

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

    [Serializable] public class ObservableIntValue : ObservableValue<int> { }
    [Serializable] public class ObservableFloatValue : ObservableValue<float> { }
    [Serializable] public class ObservableStringValue : ObservableValue<string> { }
    [Serializable] public class ObservableColor32Value : ObservableValue<Color32> { }
    [Serializable] public class ObservableCollectionValue : ObservableValue<IEnumerable> { }
    [Serializable] public class ObservableGameObjectValue : ObservableValue<GameObject> { }
    [Serializable] public class ObservableMonoBehaviourValue : ObservableValue<MonoBehaviour> { }
}