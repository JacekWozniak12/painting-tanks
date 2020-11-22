namespace PaintingTanks.Library
{
    using UnityEngine;

    public static class MathL
    {
        public static Vector3[] Vector3BaseArray = { Vector3.forward, Vector3.back, Vector3.up, Vector3.left, Vector3.down, Vector3.left };

        public readonly static int ONE_HUNDRED = 100;

        public static double Clamp(double value, double min, double max)
        {
            if (min > max) ObjectsL.Swap<double>(ref min, ref max);
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public static int Clamp(int value, int min, int max)
        {
            if (min > max) ObjectsL.Swap<int>(ref min, ref max);
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public static float Clamp(float value, float min, float max)
        {
            if (min > max) ObjectsL.Swap<float>(ref min, ref max);
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public static float Clamp01(float value) => Clamp(value, 0, 1);

        public static double Clamp01(double value) => Clamp(value, 0, 1);

        public static float ClampPositive(float value, float max) => Clamp(value, 0, max);

        public static double ClampPositive(double value, double max) => Clamp(value, 0, max);

        public static int ClampPositive(int value, int max) => Clamp(value, 0, max);

        /// <summary>
        /// Sets value from min to max, when value exceeds min, then it is set to max and vice versa
        /// </summary>
        public static int Loop(int value, int min, int max)
        {
            if (value < min) value = max;
            else if (value > max) value = min;
            return value;
        }

        /// <summary>
        /// Sets value from min to max, when value exceeds min, then it is set to max and vice versa
        /// </summary>
        public static float Loop(float value, float min, float max)
        {
            if (value < min) value = max;
            else if (value > max) value = min;
            return value;
        }

        /// <summary>
        /// Sets value from min to max, when value exceeds min, then it is set to max and vice versa
        /// </summary>
        public static double Loop(double value, double min, double max)
        {
            if (value < min) value = max;
            else if (value > max) value = min;
            return value;
        }

        /// <summary>
        /// Sets value from 0 to max, when value exceeds 0, then it is set to max and vice versa
        /// </summary>
        public static int LoopPositive(int value, int max) => Loop(value, 0, max);
    }
}