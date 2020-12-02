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
        public TextureType textureType;
        public ObservableValue<Vector2Int> Size = new ObservableValue<Vector2Int>();
        public ObservableValue<Color32> Color = new ObservableValue<Color32>();
        public LayerMask Affects = default(LayerMask);

        private void OnEnable()
        {
            Color.Changed += ctx => UpdateTexture();
            Size.Changed += ctx => UpdateTexture();
            Texture = CreateRectangleTexture();
        }

        private Texture2D CreateRectangleTexture()
            => GraphicsL.CreateMonoColorTexture(Size, Color.Value);

        private Texture2D CreateCircleTexture()
            => GraphicsL.CreateMonoColorCircleTexture(Size, Color.Value);


        [ContextMenu("Update Texture")]
        public void UpdateTexture()
        {
            switch (textureType)
            {
                case TextureType.Rectangle:
                    Texture = CreateRectangleTexture();
                    break;
                case TextureType.Circle:
                    Texture = CreateCircleTexture();
                    break;
            }
        }
    }

    public enum TextureType { Rectangle, Circle }
}
