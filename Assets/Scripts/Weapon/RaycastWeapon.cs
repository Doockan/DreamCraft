using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Services.SimpleBot;

namespace Assets.Scripts.Weapon
{
    public class RaycastWeapon : MonoBehaviour
    {
        [Header("Weapon Setup")] [SerializeField]
        private WeaponData _weaponData;

        [SerializeField] private Transform _raycastOrigin;
        [SerializeField] private Transform _raycastDestination;


        private float _maxLifetime = 10f;
        private float _accumulatedTime;
        private bool _triggerReleased = true;
        private readonly List<Bullet> _bullets = new();
        private Ray _ray;
        private RaycastHit _hitInfo;
        public bool IsFiring { get; private set; }
        public WeaponData Data => _weaponData;

        private void LateUpdate()
        {
            UpdateBullets(Time.deltaTime);
            ProcessFiring(Time.deltaTime);
        }

        public void StartFiring()
        {
            IsFiring = true;
        }

        public void StopFiring()
        {
            IsFiring = false;
            _triggerReleased = true;
        }

        private void ProcessFiring(float deltaTime)
        {
            _accumulatedTime += deltaTime;
            float fireInterval = 1f / _weaponData.FireRate;

            switch (_weaponData.FireMode)
            {
                case WeaponFireMode.FullAuto:
                    if (IsFiring)
                    {
                        while (_accumulatedTime >= fireInterval)
                        {
                            FireBullet();
                            _accumulatedTime -= fireInterval;
                        }
                    }

                    break;

                case WeaponFireMode.SemiAuto:
                    if (IsFiring && _accumulatedTime >= fireInterval && _triggerReleased)
                    {
                        FireBullet();
                        _accumulatedTime = 0f;
                        _triggerReleased = false;
                    }

                    break;

                case WeaponFireMode.Burst:
                    if (IsFiring && _accumulatedTime >= fireInterval)
                    {
                        for (int i = 0; i < 3; i++)
                            FireBullet();
                        _accumulatedTime = 0f;
                    }

                    break;

                case WeaponFireMode.Shotgun:
                    if (_triggerReleased && _accumulatedTime >= fireInterval && IsFiring)
                    {
                        FireBullet();
                        _accumulatedTime = 0f;
                        _triggerReleased = false;
                    }

                    break;
            }
        }


        private void FireBullet()
        {
            int pellets = Mathf.Max(_weaponData.Pellets, 1);
            for (int i = 0; i < pellets; i++)
            {
                Vector3 direction = (_raycastDestination.position - _raycastOrigin.position).normalized;

                if (_weaponData.SpreadAngle > 0f)
                {
                    direction = Quaternion.Euler(
                        Random.Range(-_weaponData.SpreadAngle, _weaponData.SpreadAngle),
                        Random.Range(-_weaponData.SpreadAngle, _weaponData.SpreadAngle),
                        0) * direction;
                }

                Vector3 velocity = direction * _weaponData.BulletSpeed;
                var bullet = new Bullet(_raycastOrigin.position, velocity);
                _bullets.Add(bullet);
            }
        }

        private void UpdateBullets(float deltaTime)
        {
            RemoveExpiredBullets();
            SimulateBullets(deltaTime);
        }

        private void RemoveExpiredBullets()
        {
            _bullets.RemoveAll(b => b.Time >= _maxLifetime);
        }

        private void SimulateBullets(float deltaTime)
        {
            foreach (var bullet in _bullets)
            {
                var p0 = GetBulletPosition(bullet);
                bullet.Time += deltaTime;
                var p1 = GetBulletPosition(bullet);

                RaycastBulletSegment(p0, p1, bullet);
            }
        }

        private Vector3 GetBulletPosition(Bullet bullet)
        {
            var gravity = Vector3.down * _weaponData.BulletDrop;
            return bullet.InitialPosition + (bullet.InitialVelocity * bullet.Time) +
                   (0.5f * gravity * bullet.Time * bullet.Time);
        }

        private void RaycastBulletSegment(Vector3 start, Vector3 end, Bullet bullet)
        {
            Vector3 direction = end - start;
            float distance = direction.magnitude;

            _ray.origin = start;
            _ray.direction = direction;

            if (Physics.Raycast(_ray, out _hitInfo, distance))
            {
                ProcessHit(_hitInfo, bullet);
            }
        }

        private void ProcessHit(RaycastHit hitInfo, Bullet bullet)
        {
            bullet.Time = _maxLifetime;

            var hitBox = hitInfo.collider.GetComponent<Health>();
            if (hitBox)
            {
                hitBox.TakeDamage(_weaponData.Damage, gameObject);
            }
        }
    }
}
