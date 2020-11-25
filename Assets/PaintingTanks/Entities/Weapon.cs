namespace PaintingTanks.Behaviours.Game
{
    using UnityEngine;
    using Definitions;
    using Interfaces;
    public class Weapon : MonoBehaviour, IWeapon
    {
        public ObservableFloatValue RateOfFire;
        public Transform WeaponBarrel;
        public float ProjectileSpeed = 20f;
        
        
        [SerializeField]
        private Projectile projectile;

        private bool shooting;
        private bool rateOfFire;
        private bool ready;

        public bool IsShooting() => shooting;
        public void Ready(bool isTrue) => ready = isTrue;
        public void Trigger(bool isOn) { shooting = isOn && ready; }

        private void Update()
        {
            if(shooting)
            {

            }
        }        
    }
}