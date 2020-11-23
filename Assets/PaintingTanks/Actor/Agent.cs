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
        [SerializeField] AccelerationComponent acceleration;
        [SerializeField] MovementComponent movement;

        public Agent(float rotationSpeed, float moveSpeed, AccelerationComponent acceleration, MovementComponent movement)
        {
            this.rotationSpeed = rotationSpeed;
            this.movementSpeed = moveSpeed;
            this.acceleration = acceleration;
            this.movement = movement;
        }

        public void Awake()
        {
            if (acceleration == null) acceleration = new AccelerationComponent();
            if (movement == null) movement = new MovementComponent(GetComponent<Transform>());
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
    }
}