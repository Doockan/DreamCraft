using System.Collections.Generic;
using Assets.Scripts.Weapon;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    [Header("Weapon Setup")] [SerializeField]
    private WeaponData _weaponData;

    [SerializeField] private Transform _raycastOrigin;
    [SerializeField] private Transform _raycastDestination;


    private float _maxLifetime = 10f;
    private float _accumulatedTime;
    private readonly List<Bullet> _bullets = new();
    private Ray _ray;
    private RaycastHit _hitInfo;
    public bool IsFiring { get; private set; }

    private void LateUpdate()
    {
        UpdateBullets(Time.deltaTime);
        if (IsFiring) ProcessFiring(Time.deltaTime);
    }

    public void UpdateWeapon(float deltaTime)
    {
    }

    public void StartFiring()
    {
        IsFiring = true;
        _accumulatedTime = 0f;
    }

    public void StopFiring()
    {
        IsFiring = false;
    }

    private void ProcessFiring(float deltaTime)
    {
        _accumulatedTime += deltaTime;
        float fireInterval = 1f / _weaponData.FireRate;

        while (_accumulatedTime >= 0)
        {
            FireBullet();
            _accumulatedTime -= fireInterval;
        }
    }

    private void FireBullet()
    {
        Vector3 velocity = (_raycastDestination.position - _raycastOrigin.position).normalized *
                           _weaponData.BulletSpeed;
        var bullet = new Bullet(_raycastOrigin.position, velocity);
        _bullets.Add(bullet);
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

        Debug.Log($"HIT from {_weaponData.WeaponName} = {hitInfo.collider.name}");
        // var hitBox = hitInfo.collider.GetComponent<HitBox>();
        // if (hitBox)
        // {
        //     hitBox.OnRaycastHit(this, _ray.direction);
        // }
    }
}
