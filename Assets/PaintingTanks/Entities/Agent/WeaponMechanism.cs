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

        protected virtual void PreShootMethod() {}   
        protected virtual void PostShootMethod() { }
        protected virtual void PostShoot() { }
        protected virtual void PreShoot() { }


        [Header("Gameplay settings")]
        public ObservableValue<float> RateOfFire;
        public ObservableValue<float> ProjectileSpeed = new ObservableValue<float>();
        public ObservableValue<Projectile> projectile;
        public ObservableValue<Vector3> spread = new ObservableValue<Vector3>(Vector3.zero);


        [Header("Weapon settings")]
        [SerializeField] Transform ProjectileStart;
        [SerializeField] ForceMode ForceType = ForceMode.Impulse;

        private bool shooting;
        private bool rateOfFireHandler = true;
        private bool ready = true;
    }
}