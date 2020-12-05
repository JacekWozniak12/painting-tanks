namespace PaintingTanks.Entities.Agent.WeaponTypes
{
    using System.Collections;
    using PaintingTanks.Definitions;
    using UnityEngine;

    public class BaseTurret : WeaponMechanism
    {
        public ObservableValue<Projectile> Projectile = default;

        protected override IEnumerator ShootMethod()
        {
            var a = Instantiate(Projectile.Value);
            a.transform.position = ProjectileStart.transform.position;
            var rb = a.GetComponent<Rigidbody>();
            rb.AddForce(GetVelocity(), ForceType);
            yield return new WaitForEndOfFrame();
        }
    }
}