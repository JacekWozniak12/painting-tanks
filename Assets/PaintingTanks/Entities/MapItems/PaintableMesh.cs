namespace PaintingTanks.Entities.MapItems
{
    using System.Collections;
    using UnityEngine;
    using System.Collections.Generic;

    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    /// <summary>
    /// Class that handles drawing on itself and calculating color on texture
    /// </summary>
    public class PaintableMesh : MonoBehaviour
    {
        private const string NAME_MASK_TEXTURE = "_MaskTex";
        public bool Changed { get; private set; } = true;
        public Texture2D Countable { get; private set; }
        public int PixelCount { get; private set; }
        public int IgnoredPixelCount { get; private set; }
        public float ScoreMultiplier;

        [SerializeField] FilterMode filterMode = FilterMode.Point;
        [SerializeField] float influence = 0.75f;
        [SerializeField] private int textureSize = 32;
        [SerializeField] [Range(0, 100)] float usageInPercents;
        [SerializeField] private int renderTextureDepth = 16;


        private void Awake() => Setup();

        List<Vector2> HitsToCheck = new List<Vector2>();

        public List<TexturePartInfo> GetPartOfCountableTexture()
        {
            var result = new List<TexturePartInfo>();
            foreach(var item in HitsToCheck)
            {
                result.Add(new TexturePartInfo(item, 64));
                HitsToCheck.Remove(item);
            }
            return result;
        }

        public void ChangeSettings(float usageInPercents = 0, float influence = 0.75f, float scoreMultiplier = 1, int textureSize = 32)
        {
            this.usageInPercents = usageInPercents;
            this.influence = influence;
            this.ScoreMultiplier = scoreMultiplier;
            this.textureSize = textureSize;
        }

        public void Paint(float x, float y, Texture2D brushTexture, float brushSize)
        {
            DrawTexture(x, y, brushTexture, brushSize);
            Changed = true;
        }

        public void CheckedForPaint()
        {
            if(HitsToCheck.Count == 0) Changed = false;
        }

        public IEnumerator HandleStopPainting()
        {
            RenderTexture.active = renderTexture;
            Countable.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            yield return new WaitForEndOfFrame();
            RenderTexture.active = null;
        }

        public void StopPainting() => StartCoroutine(HandleStopPainting());

        void CreateClearTexture()
        {
            AlphaMap = new Texture2D(1, 1);
            AlphaMap.SetPixel(0, 0, new Color(0, 0, 0, 0));
            AlphaMap.Apply();
        }

        private void Setup()
        {
            CreateClearTexture();
            CheckForComponents();
            SetRenderer();
            SetCountable();
            PixelCount = textureSize * textureSize;
        }

        private void CheckForComponents()
        {
            if (renderer == null) renderer = GetComponent<Renderer>() ?? throw new System.Exception("Renderer NOT FOUND");
            if (collider == null) collider = GetComponent<MeshCollider>() ?? throw new System.Exception("MeshCollider NOT FOUND");
            if (meshFilter == null) meshFilter = GetComponent<MeshFilter>() ?? throw new System.Exception("MeshFilter NOT FOUND");
#if UNITY_EDITOR
#else
        if(!meshFilter.GetComponent<MeshCollider>().sharedMesh.isReadable) throw new System.Exception(gameObject + "Mesh is not READABLE");
#endif
        }

        private void SetRenderer()
        {
            renderTexture = GetRenderTexture();
            renderTexture.filterMode = filterMode;
            renderer.material.SetTexture(NAME_MASK_TEXTURE, renderTexture);
            renderer.material.SetFloat("_Influence", influence);
        }

        private void SetCountable()
        {
            Countable = new Texture2D(textureSize, textureSize);
            Countable.filterMode = FilterMode.Point;
            Countable.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        }

        void DrawTexture(float posX, float posY, Texture2D brushTexture, float brushSize)
        {
            posX *= textureSize;
            posY *= textureSize;
            RenderTexture.active = renderTexture;
            var temp = brushSize / 2;
            GL.PushMatrix();
            GL.LoadPixelMatrix(0, textureSize, textureSize, 0);
            Graphics.DrawTexture(
                new Rect(posX - brushTexture.width / brushSize,
                    (renderTexture.height - posY) - brushTexture.height / brushSize,
                    brushTexture.width / temp, brushTexture.height / temp), brushTexture);
            GL.PopMatrix();
            if(renderTexture.width > 256) HitsToCheck.Add(new Vector2(posX, posY)); 
            RenderTexture.active = null;
        }

        RenderTexture GetRenderTexture()
        {
            var renderTexture = new RenderTexture(textureSize, textureSize, renderTextureDepth);
            Graphics.Blit(AlphaMap, renderTexture);
            return renderTexture;
        }

        private Texture2D AlphaMap;
        private new MeshCollider collider;
        private new Renderer renderer;
        private RenderTexture renderTexture;
        private MeshFilter meshFilter;
        private Texture2D MaskTex;
    }
}
