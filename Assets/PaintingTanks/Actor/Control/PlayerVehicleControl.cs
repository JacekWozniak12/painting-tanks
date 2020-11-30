using System.Threading;
namespace PaintingTanks.Actor.Control
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(Controller))]
    public partial class PlayerVehicleControl : VehicleControl
    {
        public event Action ChangedControlScheme;

        public void ChangeControlScheme(MovementScheme scheme)
        {
            if (scheme != Scheme)
            {
                ChangedControlScheme?.Invoke();
                if(Scheme == MovementScheme.Artillery) TargetPositioner.UseMouse = false;
                else TargetPositioner.UseMouse = true;
            }
        }

        protected void Start()
        {
            SetupTank();
            SetupAssaultGun();
            SetupArtillery();
            var c = Controller.Controls.Player;
            c.Fire.performed += ctx => Command?.Handle();
            TargetPositioner.PositionChanged += targeterChanged;
        }

        private void FixedUpdate()
        {
            var pressed = CheckIfPlayerMoves(Controller.Controls.Player.Move.ReadValue<Vector2>());
            var xInput = Controller.Controls.Player.Move.ReadValue<Vector2>().y;
            var yInput = Controller.Controls.Player.Move.ReadValue<Vector2>().x;

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

        private static bool CheckIfPlayerMoves(Vector2 v) => v.x > 0 || v.x < 0 || v.y > 0 || v.y < 0;

        [Header("Mouse positioning")]
        [SerializeField] Entities.PlayerItems.Targeter TargetPositioner;
        [SerializeField] float GunTurretRotationYModifier = 15;

    }
}