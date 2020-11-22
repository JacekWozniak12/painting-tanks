namespace PaintingTanks.Interfaces
{
    using System.Collections;
    using UnityEngine;

    public interface ISpawnItem
    {
        void Spawn(GameObject gameObject, Vector3 position);
        IEnumerator HandleTimeToNextSpawn();
    }
}