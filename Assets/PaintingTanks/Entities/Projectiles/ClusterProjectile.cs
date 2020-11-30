namespace PaintingTanks.Entities.Projectiles
{
    using PaintingTanks.Definitions;
    using PaintingTanks.Entities.MapItems;
    using UnityEngine;

    public class ClusterProjectile : Projectile
    {
        public ObservableValue<Projectile[]> parts;
        public ObservableValue<float> radius;

        protected override void SetupPrerequisities()
        {
           
        }

        protected override void OnHit(Collision other)
        {
             PaintableMesh.HandlePainting(other, brush.Texture, brush.Affects);
             // spawn explosion
        }

    }
}