using System;
using PaintingTanks.Behaviours.Audio;
using PaintingTanks.Definitions;
using UnityEngine;

namespace PaintingTanks.Entities.Agent.WeaponTypes
{
    [Serializable]
    public class Launcher : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem vfx;

        [SerializeField]
        private AudioClip sfx;

        [SerializeField]
        public ObservableValue<Projectile> projectile;

        void Awake()
        {
            transform = GetComponent<Transform>();
        }

        [SerializeField]
        protected new Transform transform { get; set; }

        public void Fire(Vector3 velocity, AudioPlayOnce source, ForceMode ForceType = ForceMode.Impulse)
        {
            var a = Instantiate(projectile.Value);
            a.transform.position = transform.transform.position;
            var rb = a.GetComponent<Rigidbody>();
            rb.AddForce(velocity, ForceType);
            source?.Play(sfx);
            if (vfx != null) vfx?.Play();
        }
    }
}