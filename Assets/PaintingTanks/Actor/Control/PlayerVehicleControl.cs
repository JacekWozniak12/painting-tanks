namespace PaintingTanks.Actor.Control
{
    using System;
    using PaintingTanks.Behaviours.Audio;
    using PaintingTanks.Entities.PlayerItems;
    using UnityEngine;

    [RequireComponent(typeof(Controller))]
    public partial class PlayerVehicleControl : VehicleControl
    {
        public event Action ChangedControlScheme;
        public AudioSwitcher switcher;
        public AudioClip idle;
        public AudioClip running;
        public event Action Moving;
        public event Action Stopping;

        public void ChangeControlScheme(MovementScheme scheme)
        {
            if (scheme != Scheme)
            {
                ChangedControlScheme?.Invoke();
                if (Scheme == MovementScheme.Artillery) TargetPositioner.UseMouse = false;
                else TargetPositioner.UseMouse = true;
            }
        }

        protected void Start()
        {
            Controller.Controls.Player.Move.started += ctx => RunEngine();
            Controller.Controls.Player.Move.canceled += ctx => IdleEngine();
            SetupTank();
            SetupAssaultGun();
            SetupArtillery();
            TargetPositioner.PositionChanged += targeterChanged;
        }

        private void FixedUpdate()
        {
            HandleControls();
        }
        

        public void LockVehicle(bool isTrue)
        {
            Lock = isTrue;
        }

        private bool Lock;

        public bool IsMoving()
        {
            return Body.IsMoving();
        }

        private void IdleEngine()
        {
            switcher.Switch(idle);
        }

        private void RunEngine()
        {
            switcher.Switch(running);
        }

        private void HandleControls()
        {
            if (!Lock)
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
        }

        private static bool CheckIfPlayerMoves(Vector2 v) => v.x > 0 || v.x < 0 || v.y > 0 || v.y < 0;

        [Header("Mouse positioning")]
        [SerializeField] Targeter TargetPositioner = default;
        [SerializeField] float GunTurretRotationYModifier = 15;
    }
}