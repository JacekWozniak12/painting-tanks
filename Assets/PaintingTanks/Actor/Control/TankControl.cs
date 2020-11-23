namespace PaintingTanks.Actor.Control
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Behaviours.Game;

    public class TankControl : MonoBehaviour
    {
        Agent TankBody;
        Agent Turret;
        Agent Gun;

        private void FixedUpdate()
        {
            HandleTankBodyMovement();
            HandleTankBodyRotation();
            HandleTankGun();
        }

        private void HandleTankGun()
        {
            HandleTankGunRotation();
            HandleTankTurretRotation();
        }

        private void HandleTankBodyRotation()
        {

            
        }

        private void HandleTankBodyMovement()
        {
            
        }

        private void HandleTankTurretRotation()
        {

        }

        private void HandleTankGunRotation()
        {

        }
    }

}