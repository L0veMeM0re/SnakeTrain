using UnityEngine;

public class GrenadeLauncherWeapon : WeaponBase
{
    [SerializeField] private float explosionRadius = 3f;

    protected override void Shoot(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - firePoint.position).normalized;

        GameObject grenade = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        grenade.GetComponent<GrenadeBullet>().Initialize(direction, damage, explosionRadius);
    }
}