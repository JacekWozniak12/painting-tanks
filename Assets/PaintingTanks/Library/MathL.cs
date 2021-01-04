 using UnityEngine;
 
namespace PaintingTanks.Library
{
    public static class MathL
    {
        public static Vector3 ClampMagnitude(Vector3 vector, float min, float max)
        {
            float sm = vector.sqrMagnitude;

            if (sm > max * max) return vector.normalized * max;
            if (sm < min * min) return vector.normalized * min;

            return vector;
        }

        public readonly static int ONE_HUNDRED = 100;

        public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
        {
            value.x = Mathf.Clamp(value.x, min.x, max.x);
            value.y = Mathf.Clamp(value.y, min.y, max.y);
            value.z = Mathf.Clamp(value.z, min.z, max.z);
            return value;
        }

        public static float ClampPositive(float value, float max) => Mathf.Clamp(value, 0, max);
        public static int ClampPositive(int value, int max) => Mathf.Clamp(value, 0, max);

        /// <summary>Sets value from min to max, when value exceeds min, then it is set to max and vice versa</summary>
        public static int Loop(int value, int min, int max)
        {
            if (value < min) value = max;
            else if (value > max) value = min;
            return value;
        }

        /// <summary>Sets value from min to max, when value exceeds min, then it is set to max and vice versa</summary>
        public static float Loop(float value, float min, float max)
        {
            if (value < min) value = max;
            else if (value > max) value = min;
            return value;
        }

        /// <summary>Sets value from min to max, when value exceeds min, then it is set to max and vice vers</summary>
        public static double Loop(double value, double min, double max)
        {
            if (value < min) value = max;
            else if (value > max) value = min;
            return value;
        }

        /// <summary>Sets value from 0 to max, when value exceeds 0, then it is set to max and vice versa</summary>
        public static int LoopPositive(int value, int max) => Loop(value, 0, max);
    }
}