namespace PaintingTanks.Interfaces
{
    using UnityEngine;

    public interface IMoveable
    {
        void Rotate(Vector3 value);
        void Move(Vector3 value);
    }
}