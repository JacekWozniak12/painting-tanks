namespace PaintingTanks.Actor.Control
{
    using UnityEngine;
    using Behaviours.Test;

    [RequireComponent(typeof(Controller))]
    public class PlayerTankControl : TankControl
    {
        [SerializeField] GameObject TargetPositioner;
        [SerializeField] float MinimalDistance = 2f;
        [SerializeField] LayerMask layerMask;

        // camera => into camera control
        [SerializeField] new Camera camera;

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
            var r = camera.ScreenPointToRay(Controller.Controls.Player.FindTarget.ReadValue<Vector2>());
            if (Physics.Raycast(r, out RaycastHit info, Mathf.Infinity, layerMask))
            {
                if (Vector3.Distance(info.point, TankBody.transform.position) < MinimalDistance)
                {
                    Vector3 position = Vector3.RotateTowards(TankBody.transform.position, info.point, 360, 0);
                    TargetPositioner.transform.position = position * MinimalDistance;
                }
                else TargetPositioner.transform.position = info.point;

                TankTurretRotation(deltaTime);
            }
            else return;
        }

        private void TankTurretRotation(float deltaTime)
        {
            Vector3 lookTo = TargetPositioner.transform.position - Gun.transform.position;
            
            lookTo.y *= GunTurretRotationYModifier;
            HandleTankGunRotation(lookTo, deltaTime, CheckIfPlayerMoves(Controller.Controls.Player.FindTarget.ReadValue<Vector2>()));
            
            lookTo.y = 0f;
            HandleTankTurretRotation(lookTo, deltaTime, CheckIfPlayerMoves(Controller.Controls.Player.FindTarget.ReadValue<Vector2>()));
        }

        private static bool CheckIfPlayerMoves(Vector2 v) => v.x > 0 || v.x < 0 || v.y > 0 || v.y < 0;
    }
}