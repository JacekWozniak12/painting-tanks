using System.Collections;
using PaintingTanks.Actor.Control;
using PaintingTanks.Interfaces;
using UnityEngine;

namespace PaintingTanks.Entities.Agent.WeaponTypes
{
    public class RocketLauncher : WeaponMechanism, IWeaponMechanism
    {
        public PlayerVehicleControl Vehicle;
        public Launcher[] Launchers;
        public float Delay = 0.2f;
        public int LaunchersPerShot = 2;

        private float TimeToPrepare = 1f;
        private bool prepared = false;

        private Coroutine prepare;

        private IEnumerator preparing()
        {
            yield return new WaitForSeconds(TimeToPrepare);
            prepared = true;
        }

        protected override void SetupPrerequisites()
        {
            Vehicle = VelocityProvider.VehicleControl;
        }

        protected override void SecondaryButtonPressed()
        {
            if (prepare == null)
            {
                Vehicle.LockVehicle(true);
                prepare = StartCoroutine(preparing());
            }
        }

        protected override void SecondaryButtonStopped()
        {
            StartCoroutine(HandleButtonCancellationDelay(TimeToPrepare));
        }

        protected IEnumerator HandleButtonCancellationDelay(float value)
        {
            if (prepare != null)
            {
                yield return new WaitForSeconds(value);
                if (!isShooting)
                {
                    Vehicle.LockVehicle(true);
                }
                if (prepare != null) StopCoroutine(prepare);
                prepare = null;
            }
        }

        protected override bool OtherConditions() => !Vehicle.IsMoving() || prepared;

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
                    launcher.Fire(GetVelocity(launcher.transform.position), audioSource);
                    j++;
                }
                yield return new WaitForSeconds(Delay);
            }
            Vehicle.LockVehicle(false);
            VelocityProvider.LockCursor(false);
        }
    }
}