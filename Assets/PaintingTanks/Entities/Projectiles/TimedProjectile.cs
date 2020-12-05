namespace PaintingTanks.Entities.Projectiles
{
    using System.Collections;
    using PaintingTanks.Definitions;
    using PaintingTanks.Entities.MapItems;
    using UnityEngine;

    public class TimedProjectile : Projectile
    {
        protected override void OnHit(Collision other)
        {
            if (!activated) StartCoroutine(StartCounter());
            else if (armed)
            {
                PaintableMesh.HandlePainting(other, brush.Texture, brush.Affects);
                Destroy(this.gameObject);
            }
        }

        public ObservableValue<float> time = default;
        private bool activated = false;
        private bool armed = false;

        IEnumerator StartCounter()
        {
            activated = true;
            yield return new WaitForSeconds(time);
            armed = true;
        }
    }
}