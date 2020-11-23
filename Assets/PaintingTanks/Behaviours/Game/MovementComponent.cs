namespace PaintingTanks.Behaviours.Game
{
    using Interfaces;
    using UnityEngine;
    using System;
    using Library;

    [Serializable]
    public class MovementComponent : IMoveable
    {
        public MovementComponent(Transform transform)
        {
            this.transform = transform;
        }

        Transform transform;
        public bool CanMove = true;
        public bool CanRotate = true;

        public void Rotate(Vector3 value)
        {
            if (CanRotate)
            {
                transform.Rotate(value);
                HandleRotationLimit();
            }
        }

        public Vector2 MaximalLocalRotation = default(Vector2);
        public Vector2 MinimalLocalRotation = default(Vector2);
        public bool MinimalMaximalRotation;

        private void HandleRotationLimit()
        {
            if (MinimalMaximalRotation)
            {
                var x = transform.rotation.x;
                var y = transform.rotation.y;

                x = MathL.Clamp(x, MinimalLocalRotation.x, MaximalLocalRotation.x);
                y = MathL.Clamp(y, MinimalLocalRotation.y, MaximalLocalRotation.y);

                transform.rotation = Quaternion.Euler(x, y, transform.rotation.z);
            }
        }

        public void Move(Vector3 value)
        {
            if (CanMove) transform.position += value;
        }

        public Vector3 GetCurrentPosition()
        {
            return transform.position;
        }
    }
}