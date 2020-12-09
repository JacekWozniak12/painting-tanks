namespace PaintingTanks.Behaviours.Game
{
    using Interfaces;
    using UnityEngine;
    using System;
    using PaintingTanks.Library;

    [Serializable]
    public class Movement : IMoveable
    {
        public Movement Setup(Transform transform, Rigidbody rigidbody)
        {
            this.transform = transform;
            this.rigidbody = rigidbody;
            return this;
        }

        public void Rotate(Vector3 value)
        {
            if (CanRotate)
            {
                Quaternion rotation = transform.rotation * Quaternion.Euler(value);
                rigidbody.MoveRotation(rotation);
            }
        }

        public void LookRotate(Vector3 value)
        {
            if (CanRotate)
            {
                if (value == Vector3.zero) return;
                transform.rotation = Quaternion.LookRotation(value);
            }
        }

        public bool IsRotating()
        {
            if (rigidbody.angularVelocity != Vector3.zero) return true;
            else return false;
        }

        public bool IsMoving()
        {
            if (rigidbody.velocity != Vector3.zero) return true;
            else return false;
        }


        public void Break()
        {
            if (rigidbody.velocity.magnitude > 0)
            {
                rigidbody.velocity -= (Vector3.one * 5) * Time.deltaTime;
            }
        }


        public Vector3 GetCurrentPosition()
        {
            return transform.position;
        }

        public void Move(Vector3 value)
        {
            if (CanMove) rigidbody.AddForce((transform.TransformVector(value)), ForceMode.Acceleration);
            HandleSpeedLimit();
        }

        private void HandleSpeedLimit()
        {
            if (rigidbody.velocity.magnitude > MaxSpeed)
            {
                rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, MaxSpeed);
            }
        }

        Transform transform;
        Rigidbody rigidbody;
        public bool CanMove = true;
        public bool CanRotate = true;
        public float MaxSpeed = 30f;

    }
}