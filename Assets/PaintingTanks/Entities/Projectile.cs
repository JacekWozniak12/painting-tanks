namespace PaintingTanks.Behaviours.Game
{
    using PaintingTanks.Definitions;
    using PaintingTanks.Library;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Texture2D texture;
        [SerializeField] Vector2Int size = new Vector2Int(8, 8);
        [SerializeField] private ObservableColor32Value color = new ObservableColor32Value();

        private void Awake()
        {
            color.Changed += ctx => UpdateTexture();
        }

        private Texture2D CreateDummyTexture()
        {
            return GraphicsL.CreateMonoColorTexture(size, color.Value);
        }


        [ContextMenu("Update Texture")]
        private void UpdateTexture() { texture = CreateDummyTexture(); }
    }
}