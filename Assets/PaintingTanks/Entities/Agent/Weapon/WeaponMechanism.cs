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
        public bool IsShooting() => shooting;
        public void Ready(bool isTrue) => ready = isTrue;
        public void Trigger(bool isActive) { shooting = isActive; }

        public PlayerWeaponControls VelocityProvider;

        private Vector3 GetVelocity()
        {
            return VelocityProvider.GetVelocity(ProjectileStart.transform.position, Vector3.zero, ProjectileSpeed, spread, Vector3.zero);
        }

        protected virtual void SetupPrerequisites() { }

        private void Awake()
        {
            SetupPrerequisites();
        }

        private void Update()
        {
            if (shooting)
            {
                if (ready && rateOfFireHandler)
                {
                    PreShoot();
                    StartCoroutine(Shoot());
                    PostShoot();
                }
            }
        }

        protected IEnumerator Shoot()
        {
            rateOfFireHandler = false;
            PreShootMethod();
            StartCoroutine(ShootMethod());
            PostShoot();
            yield return new WaitForSeconds(RateOfFire);
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

        private bool shooting = default;
        private bool rateOfFireHandler = true;
        private bool ready = true;
    }
}