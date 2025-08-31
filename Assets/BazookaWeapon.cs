using UnityEngine;

public class BazookaWeapon : WeaponBase
{
    protected override void Shoot(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - firePoint.position).normalized;

        GameObject projectile = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<BazookaBullet>().Initialize(direction, damage);
    }
}