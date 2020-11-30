namespace PaintingTanks.Entities.Agent.WeaponTypes
{
    using System;
    using System.Collections;
    using PaintingTanks.Interfaces;
    using UnityEngine;

    public class RocketLauncher : Weapon, IWeapon
    {
        public Launcher[] Launchers;
        public float delay = 0.2f;
        public int launchersPerShot;

        protected sealed override IEnumerator ShootMethod()
        {
            for (int i = 0; i < Launchers.Length; i += launchersPerShot)
            {
                int j = 0;
                while (j < launchersPerShot)
                {
                    Launchers[i+j].Fire();
                    j++;
                }
            }
            yield return new WaitForSeconds(delay);
        }
    }

    [Serializable]
    public class Launcher
    {
        public ParticleSystem vfx;
        public AudioClip sfx;
        public Projectile projectile;
        public Transform transform;

        public void Fire()
        {
            
        }
    }
}