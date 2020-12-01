namespace PaintingTanks.Library
{
    using UnityEngine;

    public static class KinematicsL
    {
        public static Vector3 GetVelocityFromPointsAndTime(Vector3 start, Vector3 end, float speedPerSecond, Vector3 spread)
            => GetVelocityFromPointsAndTime(start, end, speedPerSecond, spread, Physics.gravity);

        public static Vector3 GetVelocityFromPointsAndTime(Vector3 start, Vector3 end, float speedPerSecond)
            => GetVelocityFromPointsAndTime(start, end, speedPerSecond, Vector3.zero);

        public static Vector3 GetVelocityFromPointsAndTime(Vector3 start, Vector3 end, float speedPerSecond, Vector3 spread, Vector3 gravity)
        {
            if (start == end) return Vector3.zero;

            var a = MathL.GetRandomVector(spread);
            Debug.Log(a);
            end += a;

            Vector3 vector = start - end;
            Vector3 horizontal = GetHorizontalVector(vector, gravity);
            Vector3 vertical = GetVerticalVector(vector, gravity);

            float horizontalDistance = horizontal.magnitude;
            float verticalDistance = vertical.magnitude * Mathf.Sign(Vector3.Dot(vertical, -gravity));

            float timeToTarget = Vector3.Distance(end, start) / speedPerSecond;

            float horizontalSpeed = horizontalDistance / timeToTarget;
            float verticalSpeed = (verticalDistance + ((0.5f * gravity.magnitude) * (timeToTarget * timeToTarget))) / timeToTarget;

            return (horizontal.normalized * horizontalSpeed) - (gravity.normalized * verticalSpeed);
        }

        public static Vector3 GetVerticalVector(Vector3 vector, Vector3 gravityBase) => Vector3.Project(vector, gravityBase);

        public static Vector3 GetHorizontalVector(Vector3 vector, Vector3 gravityBase)
        {
            Vector3 perpendicular = Vector3.Cross(gravityBase, Vector3.Cross(vector, gravityBase));
            return Vector3.Project(vector, perpendicular);
        }

    }
}