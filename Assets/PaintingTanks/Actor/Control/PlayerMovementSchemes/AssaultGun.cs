namespace PaintingTanks.Actor.Control
{
    using System;
    using UnityEngine;

    public partial class PlayerVehicleControl : VehicleControl
    {        
        private void AssaultGunScheme(float deltaTime, float movement, float rotation, bool pressed)
        {
            var movementVector = new Vector3(0, 0, movement);
            HandleBodyMovement(movementVector, deltaTime, pressed);

            var lookTo = (TargetPositioner.transform.position - Barrel.transform.position).normalized;
            lookTo.y *= GunTurretRotationYModifier;
            HandleGunRotation(lookTo, deltaTime, true);
            lookTo.y = 0;

            var rotationVector = new Vector3(0, rotation, 0) + lookTo;
            HandleBodyRotation(rotationVector, deltaTime, targeterPositionChanged);
        }

        private void targeterChanged()
        {
            targeterPositionChanged = true;
        }

        private bool targeterPositionChanged = false;
    }
}