namespace PaintingTanks.Actor
{
    using UnityEngine;
    using Behaviours;
    using PaintingTanks.Behaviours.Game;
    using PaintingTanks.Interfaces;

    public class Agent : MonoBehaviour
    {
        [SerializeField] float rotationSpeed;
        [SerializeField] float movementSpeed;
        [SerializeField] Acceleration acceleration;
        [SerializeField] Movement movement;

        protected virtual void Awake()
        {
            if (acceleration == null) acceleration = new Acceleration();
            if (movement == null) movement = new Movement();
            movement.Setup(GetComponent<Transform>(), GetComponent<Rigidbody>());
        }

        public void Move(Vector3 v, float deltaTime, bool active = false)
        {
            var speed = acceleration.GetSpeed(deltaTime, movementSpeed, active);
            movement.Move(v * speed);
        }

        public void Rotate(Vector3 v, float deltaTime, bool active = false)
        {
            var speed = acceleration.GetSpeed(deltaTime, rotationSpeed, active);
            movement.Rotate(v * speed);
        }

        private Vector3 currentLookRotation = Vector3.zero;

        public void LookRotate(Vector3 v, float deltaTime, bool active = false)
        {
            var speed = acceleration.GetSpeed(deltaTime, rotationSpeed, active);
            currentLookRotation = Vector3.Lerp(currentLookRotation, v, speed);
            movement.LookRotate(currentLookRotation);
        }
    }
}