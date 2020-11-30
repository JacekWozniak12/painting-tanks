namespace PaintingTanks.Definitions.ScriptableObjects
{
    using PaintingTanks.Interfaces;
    using UnityEngine;

    [CreateAssetMenu(fileName = "AmmoTypeSO", menuName = "painting-tanks/AmmoTypeSO", order = 0)]
    class AmmoTypeSO : ScriptableObject, IEntity
    {
        public string Name;
        public Sprite Icon;

        public void GetImage()
        {
            throw new System.NotImplementedException();
        }

        public void GetName()
        {
            throw new System.NotImplementedException();
        }
    }
}