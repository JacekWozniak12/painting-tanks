using System.Collections;
using PaintingTanks.Definitions;
using UnityEngine;

namespace PaintingTanks.Entities.Agent.WeaponTypes
{
    public class AssaultGunTurret : WeaponMechanism
    {
        protected override void SetupPrerequisites()
        {

        }

        protected override IEnumerator ShootMethod()
        {
            var a = Instantiate(Projectile.Value);
            a.transform.position = ProjectileStart.transform.position;
            var rb = a.GetComponent<Rigidbody>();
            rb.AddForce(GetVelocity(), ForceType);
            yield return new WaitForEndOfFrame();
        }

        public ObservableValue<Projectile> Projectile = default;
    }
}
