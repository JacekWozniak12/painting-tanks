namespace PaintingTanks.Entities
{
    using System;
    using PaintingTanks.Definitions;
    using PaintingTanks.Entities.MapItems;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] protected PaintBrushHandler brush;
        [SerializeField] protected TrailRenderer trail;

        private void Awake()
        {
            SetupPrerequisities();
        }

        protected virtual void SetupPrerequisities()
        {
            throw new NotImplementedException();
        }

        void OnCollisionEnter(Collision other) => OnHit(other);

        protected virtual void OnHit(Collision other)
        {
            PaintableMesh.HandlePainting(other, brush.Texture, brush.Affects);
        }
    }
}