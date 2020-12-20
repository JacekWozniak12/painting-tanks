namespace PaintingTanks.Entities.Agent.WeaponTypes
{
    using System.Collections;
    using PaintingTanks.Definitions;
    using UnityEngine;

    public class BaseTurret : WeaponMechanism
    {
        public ObservableValue<Projectile> Projectile = default;
        public AudioClip sfx;

        protected override IEnumerator ShootMethod()
        {
            var a = Instantiate(Projectile.Value);
            a.transform.position = ProjectileStart.transform.position;
            var rb = a.GetComponent<Rigidbody>();
            rb.AddForce(GetVelocity(), ForceType);
            audioSource?.PlayOnce(sfx);
            yield return new WaitForEndOfFrame();
        }
    }
}