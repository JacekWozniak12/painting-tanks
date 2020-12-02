namespace PaintingTanks.Entities.Agent
{
    using PaintingTanks.Interfaces;
    using UnityEngine;

    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] WeaponMechanism mechanism;
        [SerializeField] WeaponMagazine magazine;

        private void Awake()
        {
            if (mechanism == null) 
            throw new System.Exception($"{mechanism} shall not be null");

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
        
    }
}