using System.Collections.Generic;
using UnityEngine;

namespace PaintingTanks.Library
{
    public static class RandomL
    {
        public static Vector3 GetRandomVector(float x, float y, float z)
                    => new Vector3(
                            Random.Range(-Mathf.Abs(x), Mathf.Abs(x)),
                            Random.Range(-Mathf.Abs(y), Mathf.Abs(y)),
                            Random.Range(-Mathf.Abs(z), Mathf.Abs(z))
                        );

        public static Vector3 GetRandomVector(Vector3 v) => GetRandomVector(v.x, v.y, v.z);

        /// <summary> Interface for weights, suggested (1, 100) </summary>
        public interface IWeight { int GetWeight(); }

        public static T GetRandom<T>(this List<T> objs) => objs[Random.Range(0, objs.Count)];
        public static T GetRandom<T>(this T[] objs) => objs[Random.Range(0, objs.Length)];

        /// <summary> From 0 to max </summary>
        public static T GetRandom<T>(this List<T> objs, int max) => GetRandom<T>(objs, 0, max);

        /// <summary> From 0 to max </summary>
        public static T GetRandom<T>(this T[] objs, int max) => GetRandom<T>(objs, 0, max);

        public static T GetRandom<T>(this List<T> objs, int min, int max)
            => objs[Random.Range(Mathf.Clamp(min, 0, objs.Count - 1), Mathf.Clamp(max, 0, objs.Count - 1))];

        public static T GetRandom<T>(this T[] objs, int min, int max)
            => objs[Random.Range(Mathf.Clamp(min, 0, objs.Length - 1), Mathf.Clamp(max, 0, objs.Length - 1))];

        public static T GetWeightedRandom<T>(this T[] objs) where T : IWeight => objs[Random.Range(0, CalculateWeightedRandom(objs))];
        public static T GetWeightedRandom<T>(this List<T> objs) where T : IWeight => objs[Random.Range(0, CalculateWeightedRandom(objs.ToArray()))];

        private static int CalculateWeightedRandom<T>(T[] objs) where T : IWeight
        {
            int WeightSum = 0;
            foreach (var obj in objs) WeightSum += obj.GetWeight();
            int rand = Random.Range(0, WeightSum);
            for (int i = 0; i < objs.Length; i++)
            {
                if (rand < objs[i].GetWeight())
                    return i;
                rand -= objs[i].GetWeight();
            }
            return -1;
        }
    }
}