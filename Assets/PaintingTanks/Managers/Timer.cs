using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using PaintingTanks.Interfaces;

namespace PaintingTanks.Managers
{
    public class Timer : MonoBehaviour, IValueProvider<float>
    {
        public float UpdateRate = 0;
        public float TimeFromBeginning = 0;
        public event Action<float> TimeUpdated;

        public void SubscribeToProvider(IValueReceiver<float> receiver)
        {
            TimeUpdated += receiver.OnChange;
        }

        [SerializeField]
        public List<TimedEvent> TimedEvents = new List<TimedEvent>();
        private void Awake() => StartCounter();
        public void StartCounter() => StartCoroutine(Counter());
        public void StopCounter() => StopCoroutine(Counter());

        IEnumerator Counter()
        {
            yield return new WaitForSecondsRealtime(UpdateRate);
            TimeFromBeginning += Time.unscaledDeltaTime + UpdateRate;
            TimeUpdated?.Invoke(TimeFromBeginning);
            yield return CheckEventList();
            StartCoroutine(Counter());
        }

        public IEnumerator CheckEventList()
        {
            foreach (var x in TimedEvents)
            {
                if (TimeFromBeginning > x.GetTime())
                {
                    x.Invoke();
                    x.Close();
                }
            }
            return null;
        }
    }

    [Serializable]
    public class TimedEvent : ITimedEvent
    {
        public float Time;
        public UnityEvent Event;
        private bool Active = true;

        public float GetTime()
        {
            return Time;
        }

        public void Invoke()
        {
            if (Active)
            {
                Event?.Invoke();
            }
        }

        public void Close() => Active = false;
    }

}