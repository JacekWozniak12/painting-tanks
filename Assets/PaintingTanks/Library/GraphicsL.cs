namespace PaintingTanks.Library
{
    using UnityEngine;

    public static class GraphicsL
    {
        public static Texture2D CreateMonoColorTexture(Vector2Int size, Color color)
        {
            var tex = new Texture2D(size.x, size.y);
            Color[] C = CreateMonoColorArray(size.x * size.y, color);
            tex.SetPixels(C);
            tex.Apply();
            return tex;
        }

        public static Color[] CreateMonoColorArray(int size, Color color)
        {
            Color[] C = new Color[size];
            for (int i = 0; i < size; i++)
            {
                C[i] = color;
            }
            return C;
        }

        public static RenderTexture CreateRenderTextureAndApplyAlpha(Vector2Int size, int depth)
        {
            var rt = CreateRenderTexture(size, depth);
            var alphaMap = CreateMonoColorTexture(new Vector2Int(1, 1), new Color(0, 0, 0, 0));
            Graphics.Blit(alphaMap, rt);
            return rt;
        }

        public static RenderTexture CreateRenderTexture(Vector2Int size, int depth)
            => new RenderTexture(size.x, size.y, depth);


        public static bool CheckIfEqualColors(Color32 colorA, Color32 colorB)
            => colorA.r == colorB.r && colorA.g == colorB.g && colorA.b == colorB.b && colorA.a == colorB.a;
            
        public static Color32[] GetPartOfArray(Color32[] input, int height, int start_x, int start_y, int finish_x, int finish_y)
        {
            Color32[] result = new Color32[(finish_x - start_x) * (finish_y - start_y)];
            int index = 0;

            for(int i = start_y; i < finish_y; i++)
            {
                for(int j = start_x; i < finish_x; j++)
                {
                    Debug.Log(index);
                    result[index++] = input[height * i + j];
                }
            }
            return result;
        }
    }
}