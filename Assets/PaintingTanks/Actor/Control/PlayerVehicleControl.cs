namespace PaintingTanks.Actor.Control
{
    using System;
    using PaintingTanks.Behaviours.Audio;
    using PaintingTanks.Entities.PlayerItems;
    using UnityEngine;

    [RequireComponent(typeof(Controller))]
    public partial class PlayerVehicleControl : VehicleControl
    {

        public AudioSwitcher audioSwitcher;
        public AudioClip idle;
        public AudioClip running;

        public event Action MoveEvent;
        public event Action StopEvent;
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

        protected void Start()
        {
            MoveEvent += RunEngine;
            StopEvent += IdleEngine;
            SetupTank();
            SetupAssaultGun();
            SetupArtillery();
            TargetPositioner.PositionChanged += targeterChanged;
        }

        private void Moving()
        {
            MoveEvent?.Invoke();
        }

        private void Stopping()
        {
            StopEvent?.Invoke();
        }

        private bool Lock;
        
        public void LockVehicle(bool isTrue) => Lock = isTrue;
        public bool IsMoving() => Body.IsMoving();
        private void IdleEngine() => audioSwitcher.Switch(idle);
        private void RunEngine() => audioSwitcher.Switch(running);

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

        private static bool CheckIfPlayerMoves(Vector2 v) => v.x > 0 || v.x < 0 || v.y > 0 || v.y < 0;

        [Header("Mouse positioning")]
        [SerializeField] Targeter TargetPositioner = default;
        [SerializeField] float GunTurretRotationYModifier = 15;
    }
}