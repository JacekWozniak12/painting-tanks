namespace PaintingTanks.Behaviours.Game
{
    using Interfaces;
    using UnityEngine;
    using System;

    [Serializable]
    public class Movement : IMoveable
    {
        public Movement Setup(Transform transform, Rigidbody rigidbody)
        {
            this.transform = transform;
            this.rigidbody = rigidbody;
            return this;
        }

        Transform transform;
        Rigidbody rigidbody;
        public bool CanMove = true;
        public bool CanRotate = true;

        public void Rotate(Vector3 value)
        {
            if (CanRotate)
            {
                Quaternion rotation = transform.rotation * Quaternion.Euler(value);
                rigidbody.MoveRotation(rotation);
                HandleRotationLimit();
            }
        }

        public void LookRotate(Vector3 value)
        {
            if (CanRotate)
            {
                if (value == Vector3.zero) return;
                else value.Normalize();
                
                transform.rotation = Quaternion.LookRotation(value);
                HandleRotationLimit();
            }
        }

        public Vector2 MaximalLocalRotation = default(Vector2);
        public Vector2 MinimalLocalRotation = default(Vector2);
        public bool MinimalMaximalRotationOfX;
        public bool MinimalMaximalRotationOfY;


        //todo
        private void HandleRotationLimit()
        {
            // var x = transform.rotation.x;
            // var y = transform.rotation.y;
            // if (MinimalMaximalRotationOfX)
            // {
            //     x = MathL.Clamp(x, MinimalLocalRotation.x, MaximalLocalRotation.x);
            //     transform.rotation = Quaternion.Euler(x, y, transform.rotation.z);
            // }
            // if (MinimalMaximalRotationOfY)
            // {
            //     y = MathL.Clamp(y, MinimalLocalRotation.y, MaximalLocalRotation.y);
            //     transform.rotation = Quaternion.Euler(x, y, transform.rotation.z);
            // }

        }

        public void Move(Vector3 value)
        {
            if (CanMove) rigidbody.AddForce((transform.TransformVector(value)), ForceMode.Acceleration);
        }

        public Vector3 GetCurrentPosition()
        {
            return transform.position;
        }
    }
}