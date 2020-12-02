namespace PaintingTanks.Entities
{
    using System;
    using PaintingTanks.Definitions;
    using PaintingTanks.Entities.MapItems;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        // todo pooling system
        public PaintBrushHandler brush;
        [SerializeField] protected TrailRenderer trail;
        [SerializeField] protected ParticleHolder hitVFX;
        [SerializeField] protected AudioClip sound;

        private void Awake()
        {
            brush.UpdateTexture();
            trail.startColor = (Color)brush.Color.Value;
            SetupPrerequisities();
        }

        protected virtual void SetupPrerequisities()
        {
            
        }

        void OnCollisionEnter(Collision other) => OnHit(other);

        protected virtual void OnHit(Collision other)
        {
            if ((brush.Affects.value & 1 << other.gameObject.layer) != 0)
            {
                PaintableMesh.HandlePainting(other, brush.Texture, brush.Affects);
                Destroy(this.gameObject);
            }
        }
    }
}