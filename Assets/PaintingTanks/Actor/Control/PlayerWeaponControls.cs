namespace PaintingTanks.Actor.Control
{
    using PaintingTanks.Entities.Agent;
    using PaintingTanks.Entities.PlayerItems;
    using PaintingTanks.Library;
    using UnityEngine;

    public class PlayerWeaponControls : MonoBehaviour
    {
        public PlayerVehicleControl VehicleControl;
        [SerializeField] Targeter TargetPositioner;
        [SerializeField] WeaponManager manager;

        public void Awake()
        {
            SetControls();
            if (manager == null) manager = GetComponent<WeaponManager>();
        }
        
        public void LockCursor(bool isTrue)
        {
            TargetPositioner.Lock = isTrue;
        }

        private void SetControls()
        {
            Controller.Controls.Player.Switch.started += ctx => manager?.Scrolling(Controller.Controls.Player.Switch.ReadValue<Vector2>().y);
            manager.FinishedSwitching += SetWeaponOptions;
            Controller.Controls.Player.Fire.performed += ctx => manager.CurrentWeapon?.GetMechanism().Trigger(true);
            Controller.Controls.Player.Fire.canceled += ctx => manager.CurrentWeapon?.GetMechanism().Trigger(false);
        }

        private void SetWeaponOptions()
        {
            if (manager.CurrentWeapon == null) Debug.Log("Weapon wasn't selected");
            else
            {
                TargetPositioner.MaxDistance.Value = manager.CurrentWeapon.GetMaximalRange();
                TargetPositioner.MinDistance.Value = manager.CurrentWeapon.GetMinimalRange();
                VehicleControl.ChangeControlScheme(manager.CurrentWeapon.GetMovementScheme());
            }
        }

        public Vector3 GetVelocity(Vector3 position, Vector3 startModifier, float speed, Vector3 spread, Vector3 endModifier)
        {
            spread = MathL.GetRandomVector(spread);
            return TargetPositioner.GetVelocity(position + startModifier, speed, spread + endModifier);
        }
    }
}