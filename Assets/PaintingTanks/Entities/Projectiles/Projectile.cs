namespace PaintingTanks.Entities
{
    using System.Collections;
    using PaintingTanks.Entities.Camera;
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
        protected Coroutine objectDestruction; 

        protected IEnumerator DestroyAfterTime(float t)
        {
            yield return new WaitForSeconds(t);
            Destroy(gameObject);
        }

        protected virtual void SetupPrerequisities() { }

        private int hitCount;

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
                if (hitCount == 0)
                {
                    if(objectDestruction != null) StopCoroutine(objectDestruction);
                    rigidbody.velocity = rigidbody.velocity /100;
                    objectDestruction = StartCoroutine(DestroyAfterTime(0.4f));
                }
                hitCount++;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            CameraShake.Instance.InduceStress(Mathf.Clamp(brush.Size.Value.x / 10, 0.025f, 0.15f));
            OnHit(other);
        }

        protected new Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            brush.UpdateTexture();
            trail.startColor = (Color)brush.Color.Value;
            SetupPrerequisities();
            objectDestruction = StartCoroutine(DestroyAfterTime(MaximalTimeOfLife));
        }
    }
}