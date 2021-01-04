using UnityEngine;

namespace PaintingTanks.Actor.Control
{
    public class VehicleControl : MonoBehaviour
    {
        protected void HandleBodyRotation(Vector3 vector, float deltaTime, bool active) => Body.Rotate(vector, deltaTime, active);
        protected void HandleBodyMovement(Vector3 vector, float deltaTime, bool active) => Body.Move(vector, deltaTime, active);
        protected void HandleWeaponRotation(Vector3 vector, float deltaTime, bool active) => Turret.LookRotate(vector, deltaTime, active);
        protected void HandleGunRotation(Vector3 vector, float deltaTime, bool active) => Barrel.LookRotate(vector, deltaTime, active);

        [Header("Settings")]
        [SerializeField] protected MovementScheme Scheme;

        [Header("Movement Agents")]
        [SerializeField] protected MovementAgent Body;
        [SerializeField] protected MovementAgent Turret;
        [SerializeField] protected MovementAgent Barrel;

    }

    public enum MovementScheme { Tank, Assault_Gun, Artillery }
}
