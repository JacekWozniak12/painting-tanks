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
        [SerializeField] WeaponManager Manager;

        public void Awake()
        {
            if (Manager == null) Manager = GetComponent<WeaponManager>();
        }
        
        public void LockCursor(bool isTrue) => TargetPositioner.Lock = isTrue;
        
        public void SetWeaponOptions()
        {
            if (Manager.CurrentWeapon == null) Debug.Log("Weapon wasn't selected");
            else
            {
                TargetPositioner.MaxDistance.Value = Manager.CurrentWeapon.GetMaximalRange();
                TargetPositioner.MinDistance.Value = Manager.CurrentWeapon.GetMinimalRange();
                VehicleControl.ChangeControlScheme(Manager.CurrentWeapon.GetMovementScheme());
            }
        }

        public Vector3 GetVelocity(Vector3 position, Vector3 startModifier, float speed, Vector3 spread, Vector3 endModifier)
        {
            spread = RandomL.GetRandomVector(spread);
            return TargetPositioner.GetVelocity(position + startModifier, speed, spread + endModifier);
        }
    }
}