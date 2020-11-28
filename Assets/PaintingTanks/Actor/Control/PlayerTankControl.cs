namespace PaintingTanks.Actor.Control
{
    using UnityEngine;
    using Behaviours.Test;

    [RequireComponent(typeof(Controller))]
    public class PlayerTankControl : TankControl
    {
        [SerializeField] Entities.PlayerItems.Targeter TargetPositioner;
        [SerializeField] float MinimalDistance = 2f;

        // ammotypes
        // vfx
        // sounds
        // ...

        protected void Start()
        {
            var c = Controller.Controls.Player;
            c.Fire.performed += ctx => Gun.Handle();
        }

        private void FixedUpdate()
        {
            HandleTankMovement(Time.fixedDeltaTime);
            HandleGunTurret(Time.fixedDeltaTime);
        }

        protected void HandleTankMovement(float deltaTime)
        {
            var player_moves = CheckIfPlayerMoves(Controller.Controls.Player.Move.ReadValue<Vector2>());
            var movementVector = new Vector3(0, 0, Controller.Controls.Player.Move.ReadValue<Vector2>().y);
            HandleTankBodyMovement(movementVector, deltaTime, player_moves);
            var rotationVector = new Vector3(0, Controller.Controls.Player.Move.ReadValue<Vector2>().x, 0);
            HandleTankBodyRotation(rotationVector, deltaTime, player_moves);
        }

        [SerializeField] float GunTurretRotationYModifier = 15;

        protected void HandleGunTurret(float deltaTime)
        {
            TankTurretRotation(deltaTime);
        }

        private void TankTurretRotation(float deltaTime)
        {
            Vector3 lookTo = (TargetPositioner.transform.position - Gun.transform.position).normalized;

            lookTo.y *= GunTurretRotationYModifier;
            HandleTankGunRotation(lookTo, deltaTime, true);

            lookTo.y = 0f;
            HandleTankTurretRotation(lookTo, deltaTime, true);
        }

        private static bool CheckIfPlayerMoves(Vector2 v) => v.x > 0 || v.x < 0 || v.y > 0 || v.y < 0;
    }
}