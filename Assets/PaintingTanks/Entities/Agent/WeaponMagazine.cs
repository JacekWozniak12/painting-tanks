using System.Security.AccessControl;
namespace PaintingTanks.Entities.Agent
{
    using System;
    using System.Collections;
    using Definitions;
    using Interfaces;
    using UnityEngine;

    [Serializable]
    public class WeaponMagazine : MonoBehaviour, IReoladable
    {
        public ObservableValue<int> AmmoPerShot;
        public ObservableValue<int> CurrentBulletCount;
        public ObservableValue<int> ClipSize;
        public ObservableValue<float> ReloadTime;

        public event Action Empty;
        public event Action ReloadFinished;
        public event Action ReloadStarted;

        public int Reload(int amount)
        {
            ReloadStarted?.Invoke();
            var needed = ClipSize - CurrentBulletCount.Value;
            var difference = amount - needed;
            StartCoroutine(HandleDelay(ReloadTime, ReloadFinished));
            if (difference <= 0)
            {
                CurrentBulletCount.Value += amount;
                return 0;
            }
            else
            {
                CurrentBulletCount.Value += amount - needed;
                return difference;
            }
        }

        protected IEnumerator HandleDelay(float time, Action e)
        {
            yield return new WaitForSeconds(time);
            e?.Invoke();
        }

        public bool CheckIfCanShoot()
        {
            bool b = CurrentBulletCount.Value < AmmoPerShot.Value;
            if (b) Empty?.Invoke();
            return b;
        }

        public void Fired()
        {
            CurrentBulletCount.Value -= AmmoPerShot.Value;
        }
    }
}