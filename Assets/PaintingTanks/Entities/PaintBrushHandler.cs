namespace PaintingTanks.Entities
{
    using System;
    using PaintingTanks.Definitions;
    using PaintingTanks.Library;
    using UnityEngine;

    [Serializable]
    public class PaintBrushHandler : MonoBehaviour
    {
        public Texture2D Texture = default(Texture2D);
        public ObservableValue<Vector2Int> Size = new ObservableValue<Vector2Int>();
        public ObservableColor32Value Color = new ObservableColor32Value();
        public LayerMask Affects = default(LayerMask);

        private void OnEnable()
        {
            Color.Changed += ctx => UpdateTexture();
            Size.Changed += ctx => UpdateTexture();
            Texture = CreateDummyTexture();
        }

        private Texture2D CreateDummyTexture()
        {
            return GraphicsL.CreateMonoColorTexture(Size, Color.Value);
        }

        [ContextMenu("Update Texture")]
        private void UpdateTexture()
        {
            Texture = CreateDummyTexture();
        }
    }
}
