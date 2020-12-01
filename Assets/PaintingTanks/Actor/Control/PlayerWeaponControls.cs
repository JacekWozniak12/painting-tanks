namespace PaintingTanks.Actor.Control
{
    using System.Collections;
    using PaintingTanks.Entities.PlayerItems;
    using PaintingTanks.Interfaces;
    using UnityEngine;

    public class PlayerWeaponControls : MonoBehaviour, IScrollable
    {
        [SerializeField] Targeter TargetPositioner;

        IEnumerator SwitchWeapon()
        {
            yield return new WaitForEndOfFrame();
        }

        public void Scrolling(float value)
        {
            throw new System.NotImplementedException();
        }

        public void ScrollDown()
        {
            throw new System.NotImplementedException();
        }

        public void ScrollUp()
        {
            throw new System.NotImplementedException();
        }
    }
}