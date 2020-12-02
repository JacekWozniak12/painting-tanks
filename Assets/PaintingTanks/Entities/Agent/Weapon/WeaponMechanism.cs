namespace PaintingTanks.Entities.Agent
{
    using UnityEngine;
    using Definitions;
    using Interfaces;
    using System.Collections;
    using System;
    using PaintingTanks.Actor.Control;

    public class WeaponMechanism : MonoBehaviour, IWeaponMechanism
    {
        public bool IsShooting() => triggerOn;
        public void Ready(bool isTrue) => ready = isTrue;
        public void Trigger(bool isActive) { triggerOn = isActive; }

        public PlayerWeaponControls VelocityProvider;

        protected Vector3 GetVelocity(Vector3 customStart)
        {
            return VelocityProvider.GetVelocity(customStart, Vector3.zero, ProjectileSpeed, spread, Vector3.zero);
        }

        protected Vector3 GetVelocity()
        {
            return GetVelocity(ProjectileStart.transform.position);
        }

        protected virtual void SetupPrerequisites() { }

        private void Awake()
        {
            SetupPrerequisites();
        }

        private void Update()
        {
            if (triggerOn)
            {
                if (ready && rateOfFireHandler && otherConditions())
                {
                    PreShoot();
                    StartCoroutine(Shoot());
                    PostShoot();
                }
            }
        }

        protected virtual bool otherConditions() => true;

        protected IEnumerator Shoot()
        {
            rateOfFireHandler = false;
            isShooting = true;
            PreShootMethod();
            StartCoroutine(ShootMethod());
            PostShootMethod();
            yield return new WaitForSeconds(RateOfFire);
            isShooting = false;
            rateOfFireHandler = true;
        }

        protected virtual IEnumerator ShootMethod()
        {
            var a = Instantiate(projectile.Value);
            a.transform.position = ProjectileStart.transform.position;
            var rb = a.GetComponent<Rigidbody>();
            rb.AddForce(GetVelocity(), ForceType);
            yield return new WaitForEndOfFrame();
        }

        protected virtual void PreShootMethod() { }
        protected virtual void PostShootMethod() { }
        protected virtual void PostShoot() { }
        protected virtual void PreShoot() { }


        [Header("Gameplay settings")]
        public ObservableValue<float> RateOfFire = default;
        public ObservableValue<float> ProjectileSpeed = default;
        public ObservableValue<Projectile> projectile = default;
        public ObservableValue<Vector3> spread = default;


        [Header("Weapon settings")]
        [SerializeField] Transform ProjectileStart = default;
        [SerializeField] ForceMode ForceType = ForceMode.Impulse;

        private bool triggerOn = default;
        private bool rateOfFireHandler = true;
        private bool ready = true;
        public bool isShooting { get; private set; }
    }
}