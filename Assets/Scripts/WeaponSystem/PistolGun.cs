using UnityEngine;

public class PistolGun : WeaponBase
{
    protected override void Shoot(Vector3 targetPosition)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Vector3 direction = (targetPosition - firePoint.position).normalized;

        bullet.GetComponent<BulletBase>().Initialize(direction, damage);
    }
}