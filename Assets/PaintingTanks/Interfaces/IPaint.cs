namespace PaintingTanks.Interfaces
{
    using UnityEngine;
    
    public interface IPaint
    {
        void Paint(RaycastHit paintHit);
        void Paint(ContactPoint contact);
    }
}