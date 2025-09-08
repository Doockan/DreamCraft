using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Weapon;

namespace Assets.Scripts.Player
{
    public class ActiveWeapon : MonoBehaviour
    {
        #region Stub

        [SerializeField] private RaycastWeapon _primary;
        [SerializeField] private RaycastWeapon _secondary;

        public void Start()
        {
            EquipWeapon(_primary);
            EquipWeapon(_secondary);
        }

        #endregion

        private RaycastWeapon _activeWeapon;

        private readonly Dictionary<WeaponSlot, RaycastWeapon> _weapons = new();

        public void Fire() => _activeWeapon?.StartFiring();
        public void StopFire() => _activeWeapon?.StopFiring();

        public void EquipWeapon(RaycastWeapon weapon)
        {
            if (_weapons.ContainsKey(weapon.Data.WeaponSlot))
                Destroy(_weapons[weapon.Data.WeaponSlot].gameObject);

            _weapons[weapon.Data.WeaponSlot] = weapon;

            if (_activeWeapon != null && _activeWeapon.Data.WeaponSlot == weapon.Data.WeaponSlot)
            {
                weapon.gameObject.SetActive(true);
                _activeWeapon = weapon;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }

        public void SwitchWeapon(WeaponSlot slot)
        {
            if (_activeWeapon != null && _activeWeapon.Data.WeaponSlot == slot) return;

            if (_weapons.TryGetValue(slot, out var weapon))
            {
                _activeWeapon?.gameObject.SetActive(false);
                weapon.gameObject.SetActive(true);
                _activeWeapon = weapon;
            }
        }
    }
}
