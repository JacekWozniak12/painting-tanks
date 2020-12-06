namespace PaintingTanks.Managers
{
    using PaintingTanks.Actor.Control;
    using PaintingTanks.Entities.Agent;
    using PaintingTanks.Entities.PlayerItems;
    using PaintingTanks.Library;
    using UnityEngine;

    public class Player : MonoBehaviour
    {
        public Color32 PlayerColor = GraphicsL.RandomColor();
        public string Name;
        public string ControlName;
        public WeaponManager Manager;
        public PlayerVehicleControl VehicleControl;
        public PlayerWeaponControls WeaponControls;
        public Targeter Target;

        private void OnEnable()
        {
            // Weapon
            Controller.Controls.Player.Reload.performed += ctx => Manager.Reload();
            Controller.Controls.Player.Fire.performed += ctx => Manager.CurrentWeapon?.GetMechanism().Trigger(true);
            Controller.Controls.Player.Fire.canceled += ctx => Manager.CurrentWeapon?.GetMechanism().Trigger(false);
            Controller.Controls.Player.Switch.started += ctx => Manager.Scrolling(Controller.Controls.Player.Switch.ReadValue<Vector2>().y);
            Manager.FinishedSwitching += WeaponControls.SetWeaponOptions;
        }

        private void FixedUpdate()
        {
            // Movement
            VehicleControl.HandleControls(Controller.Controls.Player.Move.ReadValue<Vector2>());
            Target.HandleCursor(Controller.Controls.Player.FindTarget.ReadValue<Vector2>());
        }

        // controls
        // weapons
        // player color
        // player keys 
    }

    // 
}