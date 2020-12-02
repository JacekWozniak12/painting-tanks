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

        [SerializeField] GameObject WeaponManager;
        List<GameObject> Weapons;

        IWeapon CurrentWeapon;

        public void Awake()
        {
            SetControls();
            SetWeaponManager();
            SetCurrentWeapon(index, 0);
        }

        private void SetWeaponManager()
        {
            if (WeaponManager == null) WeaponManager = new GameObject("Weapon Manager");
            if (Weapons == null) Weapons = new List<GameObject>();
            WeaponManager.transform.parent = this.gameObject.transform;
            UpdateWeapons();
        }

        private void UpdateWeapons()
        {
            Weapons.Clear();
            Weapons.AddRange(WeaponManager.GetAllChildren());
        }

        private void SetControls()
        {
            Controller.Controls.Player.Switch.started += ctx => Scrolling(Controller.Controls.Player.Switch.ReadValue<Vector2>().y);
            Controller.Controls.Player.Fire.performed += ctx => CurrentWeapon?.GetMechanism().Trigger(true);
            Controller.Controls.Player.Fire.canceled += ctx => CurrentWeapon?.GetMechanism().Trigger(false);
        }

        public Vector3 GetVelocity(Vector3 position, Vector3 startModifier, float speed, Vector3 spread, Vector3 endModifier)
        {
            spread = MathL.GetRandomVector(spread);
            return TargetPositioner.GetVelocity(position + startModifier, speed, spread + endModifier);
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
            index = MathL.LoopPositive(--index, Weapons.Count);
            SetCurrentWeapon(index, -1);
        }

        public void ScrollUp()
        {
            index = MathL.LoopPositive(++index, Weapons.Count);
            SetCurrentWeapon(index, +1);
        }

        public void SetCurrentWeapon(int index, int change)
        {
            print(index);
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
            print(CurrentWeapon);
        }

        #endregion
    }
}