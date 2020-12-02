namespace PaintingTanks.Library
{
    using UnityEngine;

    public static class UnityObjectsL
    {
        public static GameObject[] GetAllChildren(this GameObject go)
            => GetAllChildren(go.transform);

        public static GameObject[] GetAllChildren(this Transform t)
        {
            GameObject[] children = new GameObject[t.childCount];

            for (int i = 0; i < t.childCount; i++)
            {
                children[i] = t.GetChild(i).gameObject;
            }
            return children;
        }
    }
}