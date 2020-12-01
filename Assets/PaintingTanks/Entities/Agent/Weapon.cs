namespace PaintingTanks.Entities.Agent
{
    using UnityEngine;
    using Definitions;
    using Interfaces;
    using System.Collections;
    using PaintingTanks.Interfaces.Basic;

    public class Weapon : MonoBehaviour, IWeapon, ISwitchable
    {
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
            var a = Instantiate(projectile, ProjectileStart, true);
            var rb = a.GetComponent<Rigidbody>();
            rb.AddForce(ProjectileStart.forward * ProjectileSpeed);
            yield return new WaitForSeconds(0);
        }

        protected virtual void PreShootMethod() { }
        protected virtual void PostShootMethod() { }
        protected virtual void PostShoot() { }
        protected virtual void PreShoot() { }

        public void On()
        {
            throw new System.NotImplementedException();
        }

        public void Off()
        {
            throw new System.NotImplementedException();
        }

        public ObservableValue<float> RateOfFire;
        public Transform ProjectileStart;
        public ObservableValue<float> ProjectileSpeed = new ObservableValue<float>();
        [SerializeField] ForceMode ForceType = ForceMode.Impulse;

        [SerializeField] private Vector3 spread = Vector3.zero;
        [SerializeField] private Transform projectileSpawn;
        [SerializeField] private Vector3 velocity;

        [SerializeField] private Projectile projectile;

        private bool shooting;
        private bool rateOfFireHandler;
        private bool ready;
    }
}