namespace PaintingTanks.Entities.Agent
{
    using UnityEngine;
    using Definitions;
    using Interfaces;
    using System.Collections;
    using System;
    using PaintingTanks.Actor.Control;
    using PaintingTanks.Behaviours.Audio;

    public class WeaponMechanism : MonoBehaviour, IWeaponMechanism
    {
        public event Action TriggerPressed;
        public event Action ShotFired;

        public AudioPlayOnce audioSource;
        public bool IsShooting() => triggerOn;
        public void Ready(bool isTrue) => ready = isTrue;
        public void Trigger(bool isActive)
        {
            triggerOn = isActive;
            if (isActive) TriggerPressed?.Invoke();
        }

        public void SecondaryButton(bool isTrue)
        {
            if (isTrue) SecondaryButtonPressed();
            else SecondaryButtonStopped();
        }

        protected virtual bool OtherConditions() => true;
        protected virtual void SetupPrerequisites() { }
        protected virtual IEnumerator ShootMethod()
        {
            Debug.Log(GetVelocity());
            yield return new WaitForEndOfFrame();
        }

        protected virtual void PreShootMethod() { }
        protected virtual void PostShootMethod() { }
        protected virtual void AfterChecking() { }
        protected virtual void BeforeChecking() { }
        protected virtual void SecondaryButtonPressed() { }
        protected virtual void SecondaryButtonStopped() { }

        protected Vector3 GetVelocity(Vector3 customStart) => 
            VelocityProvider.GetVelocity(customStart, Vector3.zero, ProjectileSpeed, Spread, Vector3.zero);
        protected Vector3 GetVelocity() => 
            GetVelocity(ProjectileStart.transform.position);
        private void Awake() => SetupPrerequisites();

        private void Update()
        {
            if (triggerOn)
            {
                BeforeChecking();
                if (ready && rateOfFireHandler && OtherConditions())
                {
                    StartCoroutine(Shoot());
                }
                AfterChecking();
            }
        }

        private IEnumerator Shoot()
        {
            rateOfFireHandler = false;
            isShooting = true;
            PreShootMethod();
            StartCoroutine(ShootMethod());
            ShotFired?.Invoke();
            PostShootMethod();
            yield return new WaitForSeconds(RateOfFire);
            isShooting = false;
            rateOfFireHandler = true;
        }

        [Header("Gameplay settings")]
        public ObservableValue<float> RateOfFire = default;
        public ObservableValue<Vector3> Spread = default;
        public ObservableValue<float> ProjectileSpeed = default;


        [Header("Weapon settings")]
        [SerializeField] protected PlayerWeaponControls VelocityProvider;
        [SerializeField] protected Transform ProjectileStart = default;
        [SerializeField] protected ForceMode ForceType = ForceMode.Impulse;

        private bool triggerOn = default;
        private bool rateOfFireHandler = true;
        private bool ready = true;
        public bool isShooting { get; private set; }
    }
}