namespace PaintingTanks.Actor.Control
{
    using UnityEngine;

    public class VehicleControl : MonoBehaviour
    {
        protected void HandleBodyRotation(Vector3 vector, float deltaTime, bool active) => Body.Rotate(vector, deltaTime, active);
        protected void HandleBodyMovement(Vector3 vector, float deltaTime, bool active) => Body.Move(vector, deltaTime, active);
        protected void HandleWeaponRotation(Vector3 vector, float deltaTime, bool active) => Weapon.LookRotate(vector, deltaTime, active);
        protected void HandleGunRotation(Vector3 vector, float deltaTime, bool active) => Barrel.LookRotate(vector, deltaTime, active);

        [Header("Settings")]
        [SerializeField] protected MovementScheme Scheme;

        
        [Header("Movement Agents")]
        [SerializeField] protected MovementAgent Body;
        [SerializeField] protected MovementAgent Weapon;
        [SerializeField] protected MovementAgent Barrel;


        [Header("Command Agents")]
        [SerializeField] protected CommandAgent Command;
    }

    public enum MovementScheme { Tank, Assault_Gun, Artillery }
}
