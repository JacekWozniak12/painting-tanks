namespace PaintingTanks.Entities
{
    using System;
    using System.Collections;
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
        protected float MaximalTimeOfLife = 30f;

        private void Awake()
        {
            brush.UpdateTexture();
            trail.startColor = (Color)brush.Color.Value;
            SetupPrerequisities();
            StartCoroutine(DestroyAfterTime(MaximalTimeOfLife));
        }

        protected IEnumerator DestroyAfterTime(float t)
        {
            yield return new WaitForSeconds(t);
            Destroy(gameObject);
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
                if (hitVFX != null)
                {
                    var a = Instantiate(hitVFX, other.GetContact(0).point, Quaternion.Euler(other.GetContact(0).normal));
                    a.Activate();
                }
                Destroy(this.gameObject);
            }
        }
    }
}