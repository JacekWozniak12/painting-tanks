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

        protected virtual void Start()
        {
            
        }

        protected void FixedUpdate()
        {
            HandleTankBodyMovement();
            HandleTankBodyRotation();
            HandleTankGun();
        }

        protected void HandleTankGun()
        {
            HandleTankGunRotation();
            HandleTankTurretRotation();
        }

        protected void HandleTankBodyRotation()
        {


        }

        protected void HandleTankBodyMovement()
        {

        }

        protected void HandleTankTurretRotation()
        {

        }

        protected void HandleTankGunRotation()
        {

        }
    }

}