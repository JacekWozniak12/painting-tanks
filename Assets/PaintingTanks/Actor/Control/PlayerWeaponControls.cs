using System;
namespace PaintingTanks.Actor.Control
{
    using System.Collections;
    using System.Collections.Generic;
    using PaintingTanks.Entities.Agent;
    using PaintingTanks.Entities.PlayerItems;
    using PaintingTanks.Interfaces;
    using PaintingTanks.Library;
    using UnityEngine;

    public class PlayerWeaponControls : MonoBehaviour
    {
        [SerializeField] Targeter TargetPositioner;
        [SerializeField] WeaponManager manager;

        public void Awake()
        {
            SetControls();
            if (manager == null) manager = GetComponent<WeaponManager>();
        }

        private void SetControls()
        {
            Controller.Controls.Player.Switch.started += ctx => manager?.Scrolling(Controller.Controls.Player.Switch.ReadValue<Vector2>().y);
            Controller.Controls.Player.Fire.performed += ctx => manager.CurrentWeapon?.GetMechanism().Trigger(true);
            Controller.Controls.Player.Fire.canceled += ctx => manager.CurrentWeapon?.GetMechanism().Trigger(false);
        }

        public Vector3 GetVelocity(Vector3 position, Vector3 startModifier, float speed, Vector3 spread, Vector3 endModifier)
        {
            spread = MathL.GetRandomVector(spread);
            return TargetPositioner.GetVelocity(position + startModifier, speed, spread + endModifier);
        }
    }
}