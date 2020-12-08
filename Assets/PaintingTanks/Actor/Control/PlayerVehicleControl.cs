namespace PaintingTanks.Actor.Control
{
    using System;
    using PaintingTanks.Behaviours.Audio;
    using PaintingTanks.Entities.PlayerItems;
    using UnityEngine;

    [RequireComponent(typeof(Controller))]
    public partial class PlayerVehicleControl : VehicleControl
    {
        public event Action ChangedControlSchemeEvent;

        public void ChangeControlScheme(MovementScheme scheme)
        {
            if (scheme != Scheme)
            {
                ChangedControlSchemeEvent?.Invoke();
                if (Scheme == MovementScheme.Artillery) TargetPositioner.UseMouse = false;
                else TargetPositioner.UseMouse = true;
            }
        }

        public void HandleControls(Vector2 variable)
        {
            if (!Lock)
            {
                var pressed = CheckIfPlayerMoves(variable);
                var xInput = variable.y;
                var yInput = variable.x;

                switch (Scheme)
                {
                    case MovementScheme.Tank:
                        TankScheme(Time.fixedDeltaTime, xInput, yInput, pressed);
                        break;
                    case MovementScheme.Artillery:
                        ArtilleryScheme(Time.fixedDeltaTime, xInput, yInput, pressed);
                        break;
                    case MovementScheme.Assault_Gun:
                        AssaultGunScheme(Time.fixedDeltaTime, xInput, yInput, pressed);
                        break;
                }
                targeterPositionChanged = false;
            }
        }

        protected void Start()
        {
            TargetPositioner.PositionChanged += targeterChanged;
        }

        public void LockVehicle(bool isTrue) => Lock = isTrue;
        public bool IsMoving() => Body.IsMoving();

        private static bool CheckIfPlayerMoves(Vector2 v) => v.x > 0 || v.x < 0 || v.y > 0 || v.y < 0;
        private bool Lock;

        [Header("Mouse positioning")]
        [SerializeField] Targeter TargetPositioner = default;
        [SerializeField] float GunTurretRotationYModifier = 15;
    }
}