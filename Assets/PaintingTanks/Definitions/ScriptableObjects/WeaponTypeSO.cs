namespace PaintingTanks.Definitions.ScriptableObjects
{
    using PaintingTanks.Interfaces;
    using UnityEngine;

    [CreateAssetMenu(fileName = "WeaponTypeSO", menuName = "painting-tanks/WeaponTypeSO", order = 0)]
    class WeaponTypeSO : ScriptableObject, IEntity
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