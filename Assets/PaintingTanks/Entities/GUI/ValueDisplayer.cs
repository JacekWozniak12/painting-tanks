namespace PaintingTanks.Entities.GUI
{
    using Interfaces;
    using UnityEngine;

    public abstract class ValueDisplayer<T, T1> : MonoBehaviour, IValueReceiver<T> where T1 : Component
    {
        protected T1 Displayer;
        protected IValueProvider<T> Provider;
        public MonoBehaviour ProviderObject;

        private void Awake()
        {
            Displayer = GetComponent<T1>();
            if (ProviderObject != null)
            {
                if (Provider != null) Debug.LogWarning($"Provider: {Provider} is being overriden by Provided GameObject");
                Provider = ProviderObject as IValueProvider<T>;
                Provider.SubscribeToProvider(this);
            }
        }

        public void Subscribe(IValueProvider<T> provider)
        {
            Provider = provider;
            Provider.SubscribeToProvider(this);
        }

        public virtual void OnChange(T value) { }
    }

}