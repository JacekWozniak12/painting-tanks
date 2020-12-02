namespace PaintingTanks.Entities.Agent
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using PaintingTanks.Definitions;
    using PaintingTanks.Interfaces;
    using PaintingTanks.Library;
    using UnityEngine;

    public class WeaponManager : MonoBehaviour
    {
        public IWeapon CurrentWeapon { get; private set; }

        int index = 0;

        private void Awake()
        {
            if (weaponHolder == null) weaponHolder = new ObservableValue<Transform>(this.gameObject.transform);
            if (weaponHolder.Value == null) weaponHolder.Value = this.transform;
            if (Weapons == null) Weapons = new List<GameObject>();
            weaponHolder.Value.transform.parent = this.gameObject.transform;
            UpdateWeapons();
            SetCurrentWeapon(index, 0);
            weaponHolder.Changed += ctx => UpdateWeapons();
        }

        private void UpdateWeapons()
        {
            Weapons.Clear();
            Weapons.AddRange(weaponHolder.Value.GetAllChildren());
        }

        public event Action StartedSwitching;
        public event Action FinishedSwitching;
        public float switchingTime = 0;

        [SerializeField] ObservableValue<Transform> weaponHolder;
        List<GameObject> Weapons;

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
            index = MathL.LoopPositive(--index, Weapons.Count - 1);
            SetCurrentWeapon(index, -1);
        }

        public void ScrollUp()
        {
            index = MathL.LoopPositive(++index, Weapons.Count - 1);
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
                        break;
                    }
                    else
                    {
                        index += change;
                        index = MathL.LoopPositive(index, Weapons.Count - 1);
                        tempElement = Weapons[index];
                    }
                }
                while (firstElement != tempElement);
            }
            catch
            {
                Debug.Log("Player do not have any weapons");
            }
        }

    }
}
