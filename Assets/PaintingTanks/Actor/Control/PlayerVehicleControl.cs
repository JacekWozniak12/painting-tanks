using System.Threading;
namespace PaintingTanks.Actor.Control
{
    using System;
    using System.Collections;
    using PaintingTanks.Entities;
    using UnityEngine;

    [RequireComponent(typeof(Controller))]
    public partial class PlayerVehicleControl : VehicleControl
    {
        [SerializeField]
        ForceMode ftype = ForceMode.Acceleration;

        [SerializeField]
        float m = 2;

        [SerializeField]
        Vector3 spread = Vector3.zero;

        IEnumerator Test()
        {
            velocity = TargetPositioner.GetVelocity(projectileSpawn.position, 50, spread);

            var a = Instantiate(
                projectile, 
                projectileSpawn.position,
                Barrel.transform.rotation);
            
            a.GetComponent<Rigidbody>().AddForce(
                velocity * m, ftype);
            
            yield return new WaitForSeconds(2);
            StartCoroutine(Test());
        }

        public event Action ChangedControlScheme;
        public Transform projectileSpawn;
        public Vector3 projectileOffset;
        public Vector3 velocity;
        public Projectile projectile;

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
            SetupTank();
            SetupAssaultGun();
            SetupArtillery();
            var c = Controller.Controls.Player;
            c.Fire.performed += ctx => Command?.Handle();
            TargetPositioner.PositionChanged += targeterChanged;
            StartCoroutine(Test());
        }

        private void FixedUpdate()
        {
            HandleControls();
        }

        private void HandleControls()
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