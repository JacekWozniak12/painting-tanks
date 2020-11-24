namespace PaintingTanks.Actor.Control
{
    using UnityEngine;
    using Behaviours.Test;

    [RequireComponent(typeof(Controller))]
    public class PlayerTankControl : TankControl
    {
        [SerializeField]
        GameObject TargetPositioner;

        [SerializeField]
        new Camera camera;

        protected void Start()
        {
            var c = Controller.Controls.Player;
            c.Fire.performed += ctx => Gun.Handle();
            Gun.AddCommand(new TestCommand());
        }

        private void FixedUpdate()
        {
            HandleMovement(Time.fixedDeltaTime);
            HandleGunTurret(Time.fixedDeltaTime);
        }

        protected void HandleMovement(float deltaTime)
        {
            var movement_vector = new Vector3(
                0, 0, Controller.Controls.Player.Move.ReadValue<Vector2>().y);

            HandleTankBodyMovement(movement_vector, deltaTime, CheckIfPlayerMoves(Controller.Controls.Player.Move.ReadValue<Vector2>()));

            var rotation_vector = new Vector3(
                0, Controller.Controls.Player.Move.ReadValue<Vector2>().x, 0
            );

            HandleTankBodyRotation(rotation_vector, deltaTime, CheckIfPlayerMoves(Controller.Controls.Player.Move.ReadValue<Vector2>()));
        }

        [SerializeField]
        float GunTurretRotationYModifier = 15;

        protected void HandleGunTurret(float deltaTime)
        {
            var r = camera.ScreenPointToRay(Controller.Controls.Player.FindTarget.ReadValue<Vector2>());
            if (Physics.Raycast(r, out RaycastHit info))
            {
                TargetPositioner.transform.position = info.point;
                Vector3 lookTo = info.point - Gun.transform.position;
                lookTo.y *= GunTurretRotationYModifier;
                HandleTankGunRotation(lookTo, deltaTime, CheckIfPlayerMoves(Controller.Controls.Player.FindTarget.ReadValue<Vector2>()));
                lookTo.y = 0f;
                HandleTankTurretRotation(lookTo, deltaTime, CheckIfPlayerMoves(Controller.Controls.Player.FindTarget.ReadValue<Vector2>()));

            }
            else return;
        }

        private static bool CheckIfPlayerMoves(Vector2 v)
        {
            return
            v.x > 0 ||
            v.x < 0 ||
            v.y > 0 ||
            v.y < 0;
        }
    }
}