namespace PaintingTanks.Entities.MapItems
{
    using System.Collections;
    using UnityEngine;
    using System.Collections.Generic;
    using PaintingTanks.Library;

    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    /// <summary>
    /// Class that handles drawing on itself and calculating color on texture
    /// </summary>
    public class PaintableMesh : MonoBehaviour
    {
        private const string NAME_MASK_TEXTURE = "_MaskTex";
        private const string NAME_INFLUENCE = "_Influence";
        public bool Changed { get; private set; } = true;
        public Texture2D Countable { get; private set; }
        [SerializeField] FilterMode filterMode = FilterMode.Point;
        [SerializeField] float influence = 0.75f;
        public int TextureSize = 32;
        [SerializeField] private int renderTextureDepth = 16;
        List<Vector2Int> HitsToCheck = new List<Vector2Int>();

        public List<TexturePartInfo> GetChangedPartsOfCountableTexture()
        {
            var result = new List<TexturePartInfo>();
            foreach (var item in HitsToCheck)
            {
                result.Add(new TexturePartInfo(item, 32));
            }
            return result;
        }

        public void ClearChangedPartsToCheck()
        {
            HitsToCheck.Clear();
        }

        public void Paint(float x, float y, Texture2D brushTexture, float brushSize)
        {
            DrawTexture(x, y, brushTexture, brushSize);
            Changed = true;
        }

        public void CheckedForPaint()
        {
            if (HitsToCheck.Count == 0) Changed = false;
        }

        public IEnumerator HandleStopPainting()
        {
            RenderTexture.active = renderTexture;
            Countable.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            yield return new WaitForEndOfFrame();
            RenderTexture.active = null;
        }

        public void StopPainting() => StartCoroutine(HandleStopPainting());

        private void Awake()
        {
            CheckForComponents();
            SetRenderer();
            SetCountable();
        }

        private void SetCountable()
        {
            Countable = new Texture2D(TextureSize, TextureSize);
            Countable.filterMode = FilterMode.Point;
            Countable.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        }

        private void DrawTexture(float posX, float posY, Texture2D brushTexture, float brushSize)
        {
            posX *= TextureSize;
            posY *= TextureSize;
            var temp = brushSize / 2;
            var rectangle = new Rect(posX - brushTexture.width / brushSize,
                    (renderTexture.height - posY) - brushTexture.height / brushSize,
                     brushTexture.width / temp,
                     brushTexture.height / temp);

            RenderTexture.active = renderTexture;

            GL.PushMatrix();
            GL.LoadPixelMatrix(0, TextureSize, TextureSize, 0);
            Graphics.DrawTexture(rectangle, brushTexture);
            GL.PopMatrix();

            if (renderTexture.width >= 256) HitsToCheck.Add(new Vector2Int((int)posX, (int)posY));

            RenderTexture.active = null;
        }

        private new MeshCollider collider;
        private new Renderer renderer;
        private RenderTexture renderTexture;
        private MeshFilter meshFilter;
        private Texture2D MaskTex;

        private void CheckForComponents()
        {
            if (renderer == null) renderer = GetComponent<Renderer>() ?? throw new System.Exception("Renderer NOT FOUND");
            if (collider == null) collider = GetComponent<MeshCollider>() ?? throw new System.Exception("MeshCollider NOT FOUND");
            if (meshFilter == null) meshFilter = GetComponent<MeshFilter>() ?? throw new System.Exception("MeshFilter NOT FOUND");
#if UNITY_EDITOR
#else
        if(!meshFilter.GetComponent<MeshCollider>().sharedMesh.isReadable) throw new System.Exception(gameObject + "Mesh IS NOT READABLE");
#endif
        }

        private void SetRenderer()
        {
            renderTexture = GraphicsL.CreateRenderTextureAndApplyAlpha(new Vector2Int(TextureSize, TextureSize), renderTextureDepth);
            renderTexture.filterMode = filterMode;
            renderer.material.SetTexture(NAME_MASK_TEXTURE, renderTexture);
            renderer.material.SetFloat(NAME_INFLUENCE, influence);
        }
    }
}
