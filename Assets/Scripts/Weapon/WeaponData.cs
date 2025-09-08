using UnityEngine;

namespace Assets.Scripts.Weapon
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [Header("General")] public string WeaponName;
        public WeaponSlot WeaponSlot;

        [Header("Stats")] public int ClipSize = 30;
        public int FireRate = 10;
        public float BulletSpeed = 1000f;
        public float BulletDrop = 0f;
        public float Damage = 10f;

        [Header("Shot Settings")] public WeaponFireMode FireMode = WeaponFireMode.SemiAuto;
        public int Pellets = 1;
        public float SpreadAngle = 0f;
    }

    public enum WeaponFireMode
    {
        SemiAuto,
        FullAuto,
        Burst,
        Shotgun
    }
}