using UnityEngine;
using PaintingTanks.Behaviours.Game;
using System;
using PaintingTanks.Behaviours.Audio;

namespace PaintingTanks.Actor
{
    public class MovementAgent : MonoBehaviour
    {
        [SerializeField] float rotationSpeed;
        [SerializeField] float movementSpeed;
        [SerializeField] Acceleration acceleration;
        [SerializeField] Movement movement;

        public bool SoundSystem = true;

        public AudioSwitcher audioSwitcher;
        public AudioClip idle;
        public AudioClip move;
        public AudioClip rotate;

        public event Action MoveEvent;
        public event Action StopEvent;
        public event Action RotateEvent;

        protected void Start()
        {
            if (SoundSystem)
            {
                MoveEvent += StartMovementSound;
                StopEvent += StopMovementSound;
                RotateEvent += RotateMovementSound;
            }
        }

        private void StopMovementSound() => audioSwitcher.Play(idle);
        private void StartMovementSound() => audioSwitcher.Play(move);
        private void RotateMovementSound() => audioSwitcher.Play(rotate);

        protected virtual void Awake()
        {
            if (acceleration == null) acceleration = new Acceleration();
            if (movement == null) movement = new Movement();
            movement.Setup(GetComponent<Transform>(), GetComponent<Rigidbody>());
        }

        private bool previousMovementState = false;

        public bool IsMoving() => movement.IsMoving();

        public void Move(Vector3 v, float deltaTime, bool active = false)
        {
            var speed = acceleration.GetSpeed(deltaTime, movementSpeed, active);
            movement.Move(v * speed);

            if (previousMovementSpeed < 0.1f && speed != previousMovementSpeed)
            {
                RotateEvent?.Invoke();
            }
            else if (previousMovementSpeed > 0.1f && speed < 0.1)
            {
                StopEvent?.Invoke();
            }
            previousMovementSpeed = speed;
        }

        public void Break()
        {
            movement.Break();
        }

        public void Rotate(Vector3 v, float deltaTime, bool active = false)
        {
            var speed = acceleration.GetSpeed(deltaTime, rotationSpeed, active);
            movement.Rotate(v * speed);

            if (previousRotationSpeed < 0.1f && speed != previousRotationSpeed)
            {
                RotateEvent?.Invoke();
            }
            else if (previousRotationSpeed > 0.1f && speed == 0)
            {
                StopEvent?.Invoke();
            }
            previousRotationSpeed = speed;
        }

        private Vector3 currentLookRotation = Vector3.zero;

        public void LookRotate(Vector3 v, float deltaTime, bool active = false)
        {
            var speed = acceleration.GetSpeed(deltaTime, rotationSpeed, active);
            currentLookRotation = Vector3.Lerp(currentLookRotation, v, speed);
            movement.LookRotate(currentLookRotation);

            if (previousLookRotationSpeed < 0.1f && speed != previousLookRotationSpeed)
            {
                RotateEvent?.Invoke();
            }
            else if (previousLookRotationSpeed > 0.1f && speed == 0)
            {
                StopEvent?.Invoke();
            }
            previousLookRotationSpeed = speed;
        }

        private float previousMovementSpeed;
        private float previousRotationSpeed;
        private float previousLookRotationSpeed;

    }
}