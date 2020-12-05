namespace PaintingTanks.Entities.Agent.WeaponTypes
{
    using System;
    using PaintingTanks.Definitions;
    using UnityEngine;

    [Serializable]
    public class Launcher : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem vfx;

        [SerializeField]
        private AudioClip sfx;

        [SerializeField]
        public ObservableValue<Projectile> projectile;

        void Awake() {
            transform = GetComponent<Transform>();       
        }

        [SerializeField]
        protected new Transform transform {get; set;}

        public void Fire(Vector3 velocity, ForceMode ForceType = ForceMode.Impulse)
        {
            var a = Instantiate(projectile.Value);
            a.transform.position = transform.transform.position;
            var rb = a.GetComponent<Rigidbody>();
            rb.AddForce(velocity, ForceType);
            // play
            // show
        }
    }
}