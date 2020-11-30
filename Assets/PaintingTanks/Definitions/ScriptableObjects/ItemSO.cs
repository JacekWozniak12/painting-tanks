namespace PaintingTanks.Definitions.ScriptableObjects
{
    using PaintingTanks.Interfaces;
    using UnityEngine;

    [CreateAssetMenu(fileName = "ItemSO", menuName = "painting-tanks/ItemSO", order = 0)]
    class ItemSO : ScriptableObject
    {
        public IEntity item;
        public int Amount;
    }
}