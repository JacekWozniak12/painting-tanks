using PaintingTanks.Interfaces;
using UnityEngine;


namespace PaintingTanks.Definitions.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponTypeSO", menuName = "painting-tanks/WeaponTypeSO", order = 0)]
    class WeaponTypeSO : ScriptableObject, IEntity
    {
        public string Nameunit;
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