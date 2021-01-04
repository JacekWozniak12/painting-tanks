using PaintingTanks.Interfaces;
using UnityEngine;

namespace PaintingTanks.Definitions.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ItemSO", menuName = "painting-tanks/ItemSO", order = 0)]
    class ItemSO : ScriptableObject
    {
        public IEntity item;
        public int Amount;
    }
}