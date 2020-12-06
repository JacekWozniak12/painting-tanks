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

        public bool IsMoving()
        {
            if (rigidbody.velocity != Vector3.zero) return true;
            else return false;
        }

        public void Move(Vector3 value)
        {
            if (CanMove) rigidbody.AddForce((transform.TransformVector(value)), ForceMode.Acceleration);
        }

        public Vector3 GetCurrentPosition()
        {
            return transform.position;
        }

        Transform transform;
        Rigidbody rigidbody;
        public bool CanMove = true;
        public bool CanRotate = true;

    }
}