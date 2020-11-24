namespace PaintingTanks.Interfaces
{
    using System.Collections;
    using UnityEngine;

    public interface ISpawnItem
    {
        void Spawn(GameObject gameObject, Vector3 position);
        void Activate();
        void Deacvtivate();
        IEnumerator HandleTimeToNextSpawn();
    }
}