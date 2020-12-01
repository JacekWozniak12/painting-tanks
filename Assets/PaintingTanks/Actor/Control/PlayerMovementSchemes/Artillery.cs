namespace PaintingTanks.Actor.Control
{
    using System;
    using UnityEngine;

    public partial class PlayerVehicleControl : VehicleControl
    {
        private void SetupArtillery(){
            currentDistance = TargetPositioner.MaxDistance;
        }

        private void ArtilleryScheme(float deltaTime, float movement, float rotation, bool pressed)
        {
            var movementVector = new Vector3(0, 0, movement);
            HandleBodyMovement(movementVector, deltaTime, pressed);

            var rotationVector = new Vector3(0, rotation, 0);
            HandleBodyRotation(rotationVector, deltaTime, pressed);

            desiredTargetForArtillery = transform.position + transform.forward * currentDistance;
            TargetPositioner.SetPosition(desiredTargetForArtillery);
        }

        private float currentDistance = 0; 
        private Vector3 desiredTargetForArtillery = Vector3.zero;
    }
}
