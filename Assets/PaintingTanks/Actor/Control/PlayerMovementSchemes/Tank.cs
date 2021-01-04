using System;
using UnityEngine;

namespace PaintingTanks.Actor.Control
{
    public partial class PlayerVehicleControl : VehicleControl
    {
        private void TankScheme(float deltaTime, float movement, float rotation, bool pressed)
        {
            var movementVector = new Vector3(0, 0, movement);
            HandleBodyMovement(movementVector, deltaTime, pressed);
            var rotationVector = new Vector3(0, rotation, 0);
            HandleBodyRotation(rotationVector, deltaTime, pressed);
            Vector3 lookTo = (TargetPositioner.transform.position - Barrel.transform.position).normalized;
            lookTo.y *= GunTurretRotationYModifier;
            HandleGunRotation(lookTo, deltaTime, true);
            lookTo.y = 0f;
            HandleWeaponRotation(lookTo, deltaTime, true);
        }
    }
}