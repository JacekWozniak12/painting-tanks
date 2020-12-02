namespace PaintingTanks.Library
{
    using UnityEngine;

    public static class UnityObjectsL
    {
        public static GameObject[] GetAllChildren(this GameObject go)
        {
            GameObject[] children = new GameObject[go.transform.childCount];

            for(int i = 0; i < go.transform.childCount; i++)
            {
                children[i] = go.transform.GetChild(i).gameObject;
            }

            return children;
        }
    }
}