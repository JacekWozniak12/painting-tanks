using System.Collections.Generic;
using UnityEngine;

namespace PaintingTanks.Library
{
    public static class ObjectsL
    {
        public static bool IsNullOrEmpty<T>(this List<T> objs) => objs == null || objs.Count == 0;
        public static bool IsNullOrEmpty<T>(this T[] objs) => objs == null || objs.Length == 0;

        public static bool IsNullOrEmpty(this string obj)
        {
            if (obj == "" || obj == null) return true;
            return false;
        }

        public static IEnumerable<T> GetOrCreateCollection<T>(this IEnumerable<T> obj, bool warning = true)
        {
            if (obj == null)
            {
                obj = new List<T>() as IEnumerable<T>;
                if (warning) Debug.LogWarning("Collection is null, created: " + obj);
            }
            return obj;
        }

        public static void Swap<T>(ref T obj1, ref T obj2)
        {
            T tmp = obj1; obj1 = obj2; obj2 = tmp;
        }
    }
}