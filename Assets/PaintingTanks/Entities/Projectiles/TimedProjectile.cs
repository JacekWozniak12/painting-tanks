namespace PaintingTanks.Entities.Projectiles
{
    using System.Collections;
    using PaintingTanks.Definitions;
    using PaintingTanks.Entities.MapItems;
    using UnityEngine;

    public class TimedProjectile : Projectile
    {
        public ObservableValue<float> time;

        protected override void OnHit(Collision other)
        {
            PaintableMesh.HandlePainting(other, brush.Texture, brush.Affects);
            StartCoroutine(StartCounter());
        }

        IEnumerator StartCounter()
        {
            yield return new WaitForSeconds(time);
            
        }

    }
}