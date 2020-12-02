namespace PaintingTanks.Entities.Projectiles
{
    using System.Collections;
    using PaintingTanks.Definitions;
    using PaintingTanks.Entities.MapItems;
    using UnityEngine;

    public class TimedProjectile : Projectile
    {
        public ObservableValue<float> time = default;
        private bool activated = false;
        private bool armed = false;


        protected override void OnHit(Collision other)
        {
            if (!activated) StartCoroutine(StartCounter());
            else if (armed)
            {
                PaintableMesh.HandlePainting(other, brush.Texture, brush.Affects);
                Destroy(this.gameObject);
            }
        }

        IEnumerator StartCounter()
        {
            activated = true;
            yield return new WaitForSeconds(time);
        }



    }
}