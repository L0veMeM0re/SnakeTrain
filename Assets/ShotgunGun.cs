using UnityEngine;

public class ShotgunGun : WeaponBase
{
    [SerializeField] private int pelletCount = 3;       // количество пуль
    [SerializeField] private float spreadAngle = 30f;   // угол конуса

    protected override void Shoot(Vector3 targetPosition)
    {
        Vector3 baseDir = (targetPosition - firePoint.position).normalized;

        for (int i = 0; i < pelletCount; i++)
        {
            float angle = ((float)i / (pelletCount - 1) - 0.5f) * spreadAngle;

            Vector3 spreadDir = Quaternion.Euler(0, 0, angle) * baseDir;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<BulletBase>().Initialize(spreadDir, damage);
        }
    }
}