namespace PaintingTanks.Entities.Agent
{
    using PaintingTanks.Actor.Control;
    using PaintingTanks.Definitions;
    using PaintingTanks.Interfaces;
    using UnityEngine;

    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] WeaponMechanism mechanism;
        [SerializeField] WeaponMagazine magazine;
        [SerializeField] MovementScheme movementScheme;

        [SerializeField] ObservableValue<float> MinimalRange;
        [SerializeField] ObservableValue<float> MaximalRange;
        [SerializeField] ObservableValue<bool> angleConstrained;
        [SerializeField] ObservableValue<float> angle;

        private void Awake()
        {
            if (mechanism == null)
            {
                mechanism = GetComponent<WeaponMechanism>();
                if (mechanism == null) throw new System.Exception($"{mechanism} shall not be null");
            }


            if (magazine != null)
            {
                magazine.Empty += SetReadyOff;
                magazine.ReloadStarted += SetReadyOff;
                magazine.ReloadFinished += SetReadyOn;
            }
        }

        private void SetTriggerOn() => mechanism.Trigger(true);
        private void SetTriggerOff() => mechanism.Trigger(false);
        private void SetReadyOff() => mechanism.Ready(false);
        private void SetReadyOn() => mechanism.Ready(true);

        public IReoladable GetMagazine() => magazine;
        public IWeaponMechanism GetMechanism() => mechanism;

        public float GetMinimalRange() => MinimalRange;
        public float GetMaximalRange() => MaximalRange;
        public MovementScheme GetMovementScheme() => movementScheme;

        public bool GetAngleConstraint(out float angle)
        {
            angle = this.angle;
            return angleConstrained;
        }

        public bool IsFiring()
        {
            return mechanism.isShooting;
        }
    }
}