using UnityEngine;
using System.Collections;

public class FamasWeapon : WeaponBase
{
    [SerializeField] private int burstCount = 3;
    [SerializeField] private float burstDelay = 0.1f;

    protected override void Shoot(Vector3 targetPosition)
    {
        StartCoroutine(BurstFire(targetPosition));
    }

    private IEnumerator BurstFire(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - firePoint.position).normalized;

        for (int i = 0; i < burstCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<BulletBase>().Initialize(direction, damage);
            yield return new WaitForSeconds(burstDelay);
        }
    }
}