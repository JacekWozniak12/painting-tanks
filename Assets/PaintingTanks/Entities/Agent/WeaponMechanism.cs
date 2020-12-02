namespace PaintingTanks.Entities.Agent
{
    using UnityEngine;
    using Definitions;
    using Interfaces;
    using System.Collections;
    using PaintingTanks.Interfaces.Basic;
    using System;

    public class WeaponMechanism : MonoBehaviour, IWeaponMechanism
    {
        public bool IsShooting() => shooting;
        public void Ready(bool isTrue) => ready = isTrue;
        public void Trigger(bool isActive) { shooting = isActive; }

        public event Action TriggerPressed;

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
            var a = Instantiate(projectile.Value, ProjectileStart, true);
            var rb = a.GetComponent<Rigidbody>();
            rb.AddForce(ProjectileStart.forward * ProjectileSpeed);
            yield return new WaitForEndOfFrame();
        }

        protected virtual void PreShootMethod()
        {

        }
        
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
        [SerializeField] private Transform projectileSpawn;
        [SerializeField] private Vector3 velocity;

        private bool shooting;
        private bool rateOfFireHandler;
        private bool ready;
    }
}