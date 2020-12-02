namespace PaintingTanks.Entities.Agent.WeaponTypes
{
    using System.Collections;
    using PaintingTanks.Actor.Control;
    using PaintingTanks.Interfaces;
    using UnityEngine;

    public class RocketLauncher : WeaponMechanism, IWeaponMechanism
    {
        public PlayerVehicleControl Vehicle;
        public Launcher[] Launchers;
        public float Delay = 0.2f;
        public int LaunchersPerShot = 2;

        private bool LockWish = false;

        protected override void SetupPrerequisites()
        {
            Vehicle = VelocityProvider.VehicleControl;
            TriggerPressed += WishForLock;
        }

        protected override bool otherConditions()
        {
            return !Vehicle.IsMoving();
        }

        protected void WishForLock()
        {
            if (!IsShooting())
            {
                LockWish = !LockWish;
                print(LockWish);
                Vehicle.LockVehicle(LockWish);
            }
        }

        protected sealed override IEnumerator ShootMethod()
        {
            VelocityProvider.LockCursor(true);
            Vehicle.LockVehicle(true);
            for (int i = 0; i < Launchers.Length; i += LaunchersPerShot)
            {
                int j = 0;
                while (j < LaunchersPerShot)
                {
                    var launcher = Launchers[i + j];
                    launcher.Fire(GetVelocity(launcher.transform.position));
                    j++;
                }
                yield return new WaitForSeconds(Delay);
            }
            Vehicle.LockVehicle(false);
            VelocityProvider.LockCursor(false);
        }
    }
}