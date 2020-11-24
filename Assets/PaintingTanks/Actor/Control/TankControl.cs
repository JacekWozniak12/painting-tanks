namespace PaintingTanks.Actor.Control
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Behaviours.Game;

    public class TankControl : MonoBehaviour
    {
        [SerializeField]
        protected ExtendedAgent TankBody;

        [SerializeField]
        protected ExtendedAgent Turret;

        [SerializeField]
        protected ExtendedAgent Gun;

        protected void HandleTankBodyRotation(Vector3 vector, float deltaTime, bool active)
        {
            TankBody.Rotate(vector, deltaTime, active);
        }

        protected void HandleTankBodyMovement(Vector3 vector, float deltaTime, bool active)
        {
            TankBody.Move(vector, deltaTime, active);
        }

        protected void HandleTankTurretRotation(Vector3 vector, float deltaTime, bool active)
        {
            Turret.LookRotate(vector, deltaTime, active);
        }

        protected void HandleTankGunRotation(Vector3 vector, float deltaTime, bool active)
        {
            Gun.LookRotate(vector, deltaTime, active);
        }
    }

}