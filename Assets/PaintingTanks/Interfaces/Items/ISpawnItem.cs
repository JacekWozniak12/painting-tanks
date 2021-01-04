using System.Collections;
using UnityEngine;

namespace PaintingTanks.Interfaces
{
    public interface ISpawnItem
    {
        void Spawn(GameObject gameObject, Vector3 position);
        void Activate();
        void Deacvtivate();
        IEnumerator HandleTimeToNextSpawn();
    }
}