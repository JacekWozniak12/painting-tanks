using System;
using PaintingTanks.Definitions;
using PaintingTanks.Entities.MapItems;
using PaintingTanks.Library;
using UnityEngine;

namespace PaintingTanks.Entities.Projectiles
{
    public class ClusterProjectile : Projectile
    {
        public Projectile Part;
        public float Force = 0.5f;
        public int Amount = 5;

        protected override void OnHit(Collision other)
        {
            PaintableMesh.HandlePainting(other, brush.Texture, brush.Affects);
            ContactPoint first = other.GetContact(0);
            SpawnExplosion(first.point, first.normal);
        }

        private void SpawnExplosion(Vector3 position, Vector3 normal)
        {
            Projectile[] projectiles = new Projectile[Amount];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, normal);

            for (int i = 0; i < Amount; i++)
            {
                projectiles[i] = Instantiate(Part, position + Vector3.up / 10, rotation);
                projectiles[i].GetComponent<Rigidbody>().AddForce(RandomL.GetRandomVector(Vector3.one) * Force);
            }
        }
    }
}