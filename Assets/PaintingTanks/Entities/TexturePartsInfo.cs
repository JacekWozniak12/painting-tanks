namespace PaintingTanks.Entities
{
    using UnityEngine;
    using Library;

    public class TexturePartInfo
    {
        public Vector2Int Start;
        public Vector2Int Finish;
        public int Radius;

        public TexturePartInfo(Vector2Int vec1, Vector2Int vec2)
        {
            var a = vec1.x;
            var b = vec2.x;
            var c = vec1.y;
            var d = vec2.y;

            if (a > b) ObjectsL.Swap(ref a, ref b);
            if (c > d) ObjectsL.Swap(ref c, ref d);

            Start = new Vector2Int(a, c);
            Finish = new Vector2Int(b, d);
            Radius = GetSize() / 2;
        }

        public TexturePartInfo(Vector2Int a, int radius)
        {
            Start = new Vector2Int(a.x + radius, a.y + radius);
            Finish = new Vector2Int(a.x - radius, a.y - radius);
            Radius = radius;
        }

        public int GetSize()
        {
            Vector2 temp = new Vector2(Start.x, Finish.y);
            return (int)Vector2.Distance(Start, temp) * (int)Vector2.Distance(Finish, temp) /4;
        }
    }

}