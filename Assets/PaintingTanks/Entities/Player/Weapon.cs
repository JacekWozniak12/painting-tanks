namespace PaintingTanks.Behaviours.Game
{
    using UnityEngine;
    using Definitions;
    using Interfaces;
    using System.Collections.Generic;
    using System.Collections;

    public class Weapon : MonoBehaviour, IWeapon
    {
        public ObservableFloatValue RateOfFire;
        public Transform ProjectilePosition;
        public float ProjectileSpeed = 20f;

        [SerializeField] private Projectile projectile;

        private bool shooting;
        private bool rateOfFireHandler;
        private bool ready;
        
        public bool IsShooting() => shooting;
        public void Ready(bool isTrue) => ready = isTrue;
        public void Trigger(bool isActive) { shooting = isActive; }

        private void Update()
        {
            if (shooting)
            {
                if (ready && rateOfFireHandler)
                {
                    StartCoroutine(Shoot());
                }
            }
        }

        protected IEnumerator Shoot()
        {
            rateOfFireHandler = false;
            PreShoot();
            ShootMethod();
            PostShoot();
            yield return new WaitForSeconds(RateOfFire);
            rateOfFireHandler = true;
        }

        protected virtual void ShootMethod()
        {
            var a = Instantiate(projectile, ProjectilePosition, true);
            var rb = a.GetComponent<Rigidbody>();
            rb.AddForce(ProjectilePosition.forward * ProjectileSpeed);
       }

        protected virtual void PreShoot() { }

        protected virtual void PostShoot() { }
    }
}