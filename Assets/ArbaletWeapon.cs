using UnityEngine;

public class ArbaletWeapon : WeaponBase
{
    [SerializeField] private int pierceCount = 3;

    protected override void Shoot(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - firePoint.position).normalized;

        GameObject arrow = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        arrow.GetComponent<PiercingBullet>().Initialize(direction, damage, pierceCount);
    }
}