namespace PaintingTanks.Entities.Agent
{
    using UnityEngine;
    using Definitions;
    using Interfaces;
    using System.Collections.Generic;
    using System.Collections;

    public class Weapon : MonoBehaviour, IWeapon
    {
        public ObservableFloatValue RateOfFire;
        public Transform Barrel;
        public float ProjectileSpeed = 20f;

        [SerializeField] private Projectile projectile;

        private bool shooting;
        private bool rateOfFireHandler;
        private bool ready;

        public bool IsShooting() => shooting;
        public void Ready(bool isTrue) => ready = isTrue;
        public void Trigger(bool isActive) { shooting = isActive; }

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
            var a = Instantiate(projectile, Barrel, true);
            var rb = a.GetComponent<Rigidbody>();
            rb.AddForce(Barrel.forward * ProjectileSpeed);
            yield return new WaitForSeconds(0);
        }

        protected virtual void PreShootMethod() { }
        protected virtual void PostShootMethod() { }
        protected virtual void PostShoot() { }
        protected virtual void PreShoot() { }
    }
}