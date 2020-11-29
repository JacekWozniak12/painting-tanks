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
            BaseRotation = transform.rotation.eulerAngles;
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

        private void HandleRotationLimit(Vector3 value)
        {
            if (MinimalMaximalRotationOfX)
            {
                value.x = RotationL.ClampAngle(value.x, BaseRotation.x + MinimalLocalRotation.x, BaseRotation.x + MaximalLocalRotation.x);
            }
            if (MinimalMaximalRotationOfY)
            {
                value.y = RotationL.ClampAngle(value.y, BaseRotation.y + MinimalLocalRotation.y, BaseRotation.y + MaximalLocalRotation.y);
            }
        }

        public void Move(Vector3 value)
        {
            if (CanMove) rigidbody.AddForce((transform.TransformVector(value)), ForceMode.Acceleration);
        }

        public Vector3 GetCurrentPosition()
        {
            return transform.position;
        }

        public Vector2 MaximalLocalRotation = default(Vector2);
        public Vector2 MinimalLocalRotation = default(Vector2);
        private Vector3 BaseRotation; 
        public bool MinimalMaximalRotationOfX;
        public bool MinimalMaximalRotationOfY;
        Transform transform;
        Rigidbody rigidbody;
        public bool CanMove = true;
        public bool CanRotate = true;

    }
}