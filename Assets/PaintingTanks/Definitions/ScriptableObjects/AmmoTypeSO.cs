namespace PaintingTanks.Definitions.ScriptableObjects
{
    using System;
    using PaintingTanks.Interfaces;
    using UnityEngine;

    [CreateAssetMenu(fileName = "AmmoTypeSO", menuName = "painting-tanks/AmmoTypeSO", order = 0)]
    public class AmmoTypeSO : ScriptableObject, IEntity
    {
        public string Name;
        public Sprite Icon;
        public int Amount = 100;

        public AmmoType CreateAmmoType()
        {
            return new AmmoType(Name, Amount);
        }

        public void GetImage()
        {
            throw new System.NotImplementedException();
        }

        public void GetName()
        {
            throw new System.NotImplementedException();
        }
    }

    [Serializable]
    public class AmmoType
    {
        public AmmoType(string name, int amount)
        {
            Name = name;
            DefaultAmount = amount;
        } 

        public string Name {get; private set;}

        public int DefaultAmount = 100;
    }
}