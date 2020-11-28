namespace PaintingTanks.Entities
{
    using PaintingTanks.Definitions;
    using PaintingTanks.Entities.MapItems;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] PaintBrushHandler brush;

        private void Awake()
        {
            
        }

        void OnCollisionEnter(Collision other)
        {
            PaintableMesh.HandlePainting(other, brush.Texture, brush.Affects);
        }
    }
}