namespace PaintingTanks.Library
{
    using UnityEngine;
    using UnityEngine.Experimental.Rendering;

    public static class GraphicsL
    {
        public static readonly Color32 fullAlpha = new Color32(0, 0, 0, 0);

        public static Color32 RandomColor() => new Color32((byte)Random.Range(0, 255),
                                                           (byte)Random.Range(0, 255),
                                                           (byte)Random.Range(0, 255), 255);

        public static Color32 GetFromHSV(float hue, float saturation, float value) 
        {
            hue = MathL.Clamp(hue, 0, 360);
            saturation = MathL.Clamp01(saturation);
            value = MathL.Clamp01(saturation);
            return (Color32) Color.HSVToRGB(hue / 360, saturation, value);
        }
        
        public static Texture2D CreateMonoColorTexture(Vector2Int size, Color32 color)
        {
            var tex = new Texture2D(size.x, size.y);
            Color32[] C = CreateMonoColorArray(size.x * size.y, color);
            tex.SetPixels32(C);
            tex.Apply();
            return tex;
        }

        public static Texture2D CreateMonoColorCircleTexture(Vector2Int size, Color32 color)
        {
            var tex = new Texture2D(size.x, size.y);
            Color32[] C = CreateMonoColorCircleArray(size.x * size.y, color);
            tex.SetPixels32(C);
            tex.Apply();
            return tex;
        }

        public static Color32[] CreateMonoColorCircleArray(int size, Color32 color)
        {
            Color32[] C = new Color32[size];
            int t = (int)Mathf.Sqrt(size);

            int index = 0;
            for (int i = 0; i < t; i++)
            {
                for (int j = 0; j < t; j++)
                {
                    if (i * i + j * j <= size)
                    {
                        C[index++] = color;
                    }
                    else C[index++] = fullAlpha;
                }
            }
            return C;
        }

        public static Color32[] CreateMonoColorArray(int size, Color32 color)
        {
            Color32[] C = new Color32[size];
            for (int i = 0; i < size; i++) C[i] = color;
            return C;
        }

        public static Texture2D CreateAlphaTexture(Vector2Int size = default(Vector2Int))
            => CreateMonoColorTexture(size, new Color32(0, 0, 0, 0));

        public static RenderTexture CreateRenderTextureAndApplyAlpha(Vector2Int size, int depth)
        {
            var rt = CreateRenderTexture(size, depth);
            var alphaMap = CreateMonoColorTexture(new Vector2Int(1, 1), new Color32(0, 0, 0, 0));
            Graphics.Blit(alphaMap, rt);
            return rt;
        }

        public static RenderTexture CreateRenderTexture(Vector2Int size, int depth, GraphicsFormat graphicsFormat = GraphicsFormat.R8G8B8A8_UNorm)
            => new RenderTexture(size.x, size.y, depth, graphicsFormat);

        public static bool CheckIfEqualColors(Color32 colorA, Color32 colorB)
            => colorA.r == colorB.r && colorA.g == colorB.g && colorA.b == colorB.b && colorA.a == colorB.a;

        public static Color32[] GetPartOfArray(Color32[] input, int height, int start_x, int start_y, int finish_x, int finish_y)
        {
            Color32[] result = new Color32[(finish_x - start_x) * (finish_y - start_y)];
            int index = 0;
            for (int i = start_y; i < finish_y; i++)
            {
                for (int j = start_x; i < finish_x; j++)
                {
                    result[index++] = input[height * i + j];
                }
            }
            return result;
        }
    }
}