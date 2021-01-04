using UnityEngine;

namespace PaintingTanks.Interfaces
{
    public interface IMoveable
    {
        void Rotate(Vector3 value);
        void Move(Vector3 value);
    }
}