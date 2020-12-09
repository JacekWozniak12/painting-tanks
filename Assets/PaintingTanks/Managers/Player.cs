namespace PaintingTanks.Managers
{
    using PaintingTanks.Actor.Control;
    using PaintingTanks.Entities.Agent;
    using PaintingTanks.Entities.PlayerItems;
    using PaintingTanks.Library;
    using UnityEngine;

    public class Player : MonoBehaviour
    {
        public Color32 PlayerColor = GraphicsL.fullAlpha;
        public string Name;
        public string ControlName;
        public WeaponManager Manager;
        public PlayerVehicleControl VehicleControl;
        public PlayerWeaponControls WeaponControls;
        public Targeter Target;


        private void Awake()
        {
            if (PlayerColor.Equals(GraphicsL.fullAlpha)) GraphicsL.RandomColor();
        }

        private void OnEnable()
        {
            // Weapon
            Controller.Controls.Player.Reload.performed += ctx => Manager.Reload();
            Manager.FinishedSwitching += WeaponControls.SetWeaponOptions;
            Controller.Controls.Player.Fire.performed += ctx => Manager.CurrentWeapon?.GetMechanism().Trigger(true);
            Controller.Controls.Player.Fire.canceled += ctx => Manager.CurrentWeapon?.GetMechanism().Trigger(false);
            Controller.Controls.Player.Switch.started += ctx => Manager.Scrolling(Controller.Controls.Player.Switch.ReadValue<Vector2>().y);
            Controller.Controls.Player.Special.started += ctx => Manager.CurrentWeapon?.GetMechanism().SecondaryButton(true);
            Controller.Controls.Player.Special.canceled += ctx => Manager.CurrentWeapon?.GetMechanism().SecondaryButton(false);
            WeaponControls.SetWeaponOptions();

            Controller.Controls.Player.Break.performed += ctx => VehicleControl.Break();
        }

        private void Update()
        {
            Target.HandleCursor(Controller.Controls.Player.FindTarget.ReadValue<Vector2>());
        }

        private void FixedUpdate()
        {
            VehicleControl.HandleControls(Controller.Controls.Player.Move.ReadValue<Vector2>());
        }
    }
}