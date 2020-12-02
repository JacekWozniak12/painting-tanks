using System;
namespace PaintingTanks.Actor.Control
{
    using System.Collections;
    using System.Collections.Generic;
    using PaintingTanks.Entities.PlayerItems;
    using PaintingTanks.Interfaces;
    using PaintingTanks.Library;
    using UnityEngine;

    public class PlayerWeaponControls : MonoBehaviour, IScrollable
    {
        [SerializeField] Targeter TargetPositioner;

        int index = 0;

        List<GameObject> Weapons;
        GameObject WeaponManager;

        IWeapon CurrentWeapon;

        public void Awake()
        {
            Controller.Controls.Player.Switch.started += ctx => Scrolling(Controller.Controls.Player.Switch.ReadValue<Vector2>().y);
            Controller.Controls.Player.Fire.performed += ctx => CurrentWeapon?.GetMechanism().Trigger(true);
            Controller.Controls.Player.Fire.canceled += ctx => CurrentWeapon?.GetMechanism().Trigger(false);
            if (WeaponManager == null) WeaponManager = new GameObject("Weapon Manager");
            if (Weapons == null) Weapons = new List<GameObject>();
            WeaponManager.transform.parent = this.gameObject.transform;
            SetCurrentWeapon(index, 0);
        }


        #region Switching

        public event Action StartedSwitching;
        public event Action FinishedSwitching;
        public float switchingTime = 0;

        IEnumerator SwitchWeapon(IWeapon weapon)
        {
            yield return new WaitForSeconds(switchingTime);
            FinishedSwitching?.Invoke();
            CurrentWeapon = weapon;
        }

        public void Scrolling(float value)
        {
            if (value > 0) ScrollUp();
            else if (value < 0) ScrollDown();
        }

        public void ScrollDown()
        {
            index = MathL.LoopPositive(index, Weapons.Count);
            SetCurrentWeapon(index, -1);
        }

        public void ScrollUp()
        {
            index = MathL.LoopPositive(index, Weapons.Count);
            SetCurrentWeapon(index, +1);
        }

        public void SetCurrentWeapon(int index, int change)
        {
            try
            {
                var firstElement = Weapons[index];
                var tempElement = Weapons[index];
                do
                {
                    if (tempElement.GetComponent<IWeapon>() is IWeapon weapon)
                    {
                        StartedSwitching?.Invoke();
                        StartCoroutine(SwitchWeapon(weapon));
                    }
                    else
                    {
                        index += change;
                        index = MathL.LoopPositive(index, Weapons.Count);
                    }
                }
                while (firstElement != tempElement);
            }
            catch
            {
                Debug.Log("Player do not have any weapons");
            }
        }

        #endregion
    }
}