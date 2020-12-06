namespace PaintingTanks.Entities.Agent
{
    using System;
    using System.Collections;
    using Definitions;
    using Interfaces;
    using PaintingTanks.Behaviours.Audio;
    using PaintingTanks.Definitions.ScriptableObjects;
    using UnityEngine;

    [Serializable]
    public class WeaponMagazine : MonoBehaviour, IReoladable
    {
        public AmmoTypeSO AmmoTypeSO;
        public ObservableValue<int> AmmoPerShot;
        public ObservableValue<int> CurrentBulletCount;
        public ObservableValue<int> MagazineSize;
        public ObservableValue<AmmoType> AmmoType;
        public ObservableValue<float> ReloadTime;

        public event Action Empty;
        public event Action ReloadFinished;
        public event Action ReloadStarted;

        public AudioClip empty;
        public AudioClip reload;
        public AudioPlayOnce source;

        private void Awake()
        {
            AmmoType.Value = AmmoTypeSO.CreateAmmoType();
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
            CheckIfCanShoot();
        }

        public int Reload(int amount)
        {
            if (amount <= 0) return 0;
            else
            {
                ReloadStarted?.Invoke();
                source?.PlayOnce(empty);
                StartCoroutine(HandleDelay(ReloadTime, ReloadFinished));
                var needed = MagazineSize - CurrentBulletCount.Value;
                var difference = amount - needed;
                
                if (difference <= 0)
                {
                    CurrentBulletCount.Value += amount;
                    return 0;
                }
                else
                {
                    CurrentBulletCount.Value += needed;
                    return difference;
                }
            }
        }

        protected IEnumerator HandleDelay(float time, Action e)
        {
            yield return new WaitForSeconds(time);
            e?.Invoke();
        }

        AmmoType IReoladable.GetAmmoType()
        {
            return AmmoType.Value;
        }
    }
}