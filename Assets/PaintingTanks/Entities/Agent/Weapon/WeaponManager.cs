using System;
using System.Collections;
using System.Collections.Generic;
using PaintingTanks.Definitions;
using PaintingTanks.Definitions.ScriptableObjects;
using PaintingTanks.Interfaces;
using PaintingTanks.Library;
using UnityEngine;

namespace PaintingTanks.Entities.Agent
{
    public class WeaponManager : MonoBehaviour
    {
        public IWeapon CurrentWeapon { get; private set; }
        public PaintBrushHandler paintBrushHandler;
        public Dictionary<AmmoType, int> AmmoTypes = new Dictionary<AmmoType, int>();
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
            WeaponAmmoManagment();
        }

        private void WeaponAmmoManagment()
        {
            foreach (GameObject weaponObject in Weapons)
            {
                if (weaponObject.GetComponent<IWeapon>() is IWeapon weapon)
                {
                    if (weapon.GetMagazine() is IReoladable reoladable)
                    {
                        if (!AmmoTypes.TryGetValue(reoladable.GetAmmoType(), out int v))
                        {
                            AmmoTypes.Add(reoladable.GetAmmoType(), reoladable.GetAmmoType().DefaultAmount);
                        }
                    }
                }
            }
        }

        public event Action StartedSwitching;
        public event Action FinishedSwitching;
        public float switchingTime = 0;

        [SerializeField] ObservableValue<Transform> weaponHolder;
        List<GameObject> Weapons;

        IEnumerator SwitchWeapon(IWeapon weapon)
        {
            yield return new WaitForSeconds(switchingTime);
            CurrentWeapon = weapon;
            FinishedSwitching?.Invoke();
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

        public void Reload()
        {
            if (AmmoTypes.TryGetValue(CurrentWeapon.GetMagazine().GetAmmoType(), out int ammo))
            {
                ammo = CurrentWeapon.GetMagazine().Reload(ammo);
            }
        }

        private void FixedUpdate()
        {
            CheckForSwitchPromise();
        }

        int promised = -1;
        int promisedChange = 0;

        private void CheckForSwitchPromise()
        {
            try
            {
                if (CanBeChanged())
                {
                    if (promised != -1)
                    {
                        SetCurrentWeapon(promised, promisedChange);
                        promised = -1;
                        promisedChange = 0;
                    }
                }
            }
            catch { }
        }

        private bool CanBeChanged()
        {
            return 
            !CurrentWeapon.GetMechanism().IsTriggered() || 
            !CurrentWeapon.IsFiring();
        }

        public void SetCurrentWeapon(int index, int change)
        {
            if (CurrentWeapon != null)
            {
                if (!CanBeChanged())
                {
                    promised = index;
                    promisedChange = change;
                    return;
                }
            }
            try
            {
                GameObject firstElement = Weapons[index];
                GameObject tempElement = Weapons[index];
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
