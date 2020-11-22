namespace PaintingTanks.Entities
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Library;

    public class TexturePartInfo
    {
        public Vector2 Start;
        public Vector2 Finish;

        public TexturePartInfo(Vector2 a, Vector2 b)
        {
            if (a.y > b.y) ObjectsL.Swap(ref a.y, ref b.y);
            if (a.x > b.x) ObjectsL.Swap(ref a.x, ref b.x);

            Start = a;
            Finish = b;
        }

        public TexturePartInfo(Vector2 a, float radius)
        {
            Start = new Vector2(a.x - radius, a.y - radius);
            Finish = new Vector2(a.x + radius, a.y + radius);
        }
    }

}