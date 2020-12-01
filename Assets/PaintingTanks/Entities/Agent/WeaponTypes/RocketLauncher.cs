namespace PaintingTanks.Entities.Agent.WeaponTypes
{
    using System;
    using System.Collections;
    using PaintingTanks.Definitions;
    using PaintingTanks.Interfaces;
    using UnityEngine;

    public class RocketLauncher : Weapon, IWeapon
    {
        public Launcher[] Launchers;
        public float Delay = 0.2f;
        public int LaunchersPerShot;

        protected sealed override IEnumerator ShootMethod()
        {
            for (int i = 0; i < Launchers.Length; i += LaunchersPerShot)
            {
                int j = 0;
                while (j < LaunchersPerShot)
                {
                    //todo
                    Launchers[i + j].Fire(transform.forward);
                    j++;
                }
            }
            yield return new WaitForSeconds(Delay);
        }
    }

    [Serializable]
    public class Launcher
    {
        [SerializeField]
        private ParticleSystem vfx;

        [SerializeField]
        private AudioClip sfx;

        [SerializeField]
        public ObservableValue<Projectile> projectile;

        [SerializeField]
        private Transform transform;

        public void Fire(Vector3 velocity)
        {

        }

        public void Setup(Transform parent)
        {

        }
    }
}