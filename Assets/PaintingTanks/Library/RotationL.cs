namespace PaintingTanks.Library
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class RotationL
    {
        public static float ClampAngle(float angle, float min, float max)
        {
            angle = HandleAngleExceeds(angle);
            min = HandleAngleExceeds(min);
            max = HandleAngleExceeds(max);
            return Mathf.Clamp(angle, min, max);
        }

        public static float HandleAngleExceeds(float angle)
        {
            NormalizeAngle(angle);
            if (angle > 180) angle -= 360;
            else if (angle < -180) angle += 360;
            return angle;
        }

        public static float NormalizeAngle(float angle)
        {
            while (angle > 360) angle -= 360;
            while (angle < 0) angle += 360;
            return angle;
        }
    }
}