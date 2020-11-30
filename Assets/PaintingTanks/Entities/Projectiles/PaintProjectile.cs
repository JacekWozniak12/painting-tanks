namespace PaintingTanks.Entities.Projectiles
{
    using PaintingTanks.Entities.MapItems;
    using UnityEngine;

    public class PaintProjectile : Projectile
    {

        
        protected override void OnHit(Collision other)
        {
            PaintableMesh.HandlePainting(other, brush.Texture, brush.Affects);
        }

    }
}