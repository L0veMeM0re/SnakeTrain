using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SnakeGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;   // Префаб пули
    [SerializeField] private Transform firePoint;       // Точка выстрела
    [SerializeField] private float fireRate = 1f;       // Скорострельность
    [SerializeField] private float detectionRange = 10f; // Радиус поиска врага

    private float fireCooldown = 0f;

    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            Transform target = FindClosestEnemy();
            if (target != null)
            {
                Shoot(target.position);
                fireCooldown = 1f / fireRate;
            }
        }
    }

    /// <summary>
    /// Поиск ближайшего врага (EnemyFollow и EnemyHead).
    /// </summary>
    private Transform FindClosestEnemy()
    {
        List<Transform> enemies = new List<Transform>();

        enemies.AddRange(FindObjectsOfType<EnemyFollow>().Select(e => e.transform));

        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.position);
            if (dist < closestDistance && dist <= detectionRange)
            {
                closestDistance = dist;
                closest = enemy;
            }
        }

        return closest;
    }

    /// <summary>
    /// Выстрел в направлении цели (может промахнуться, если враг двигается).
    /// </summary>
    private void Shoot(Vector3 targetPosition)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Направление фиксируется в момент выстрела (без автотрекинга пули)
        Vector3 direction = (targetPosition - firePoint.position).normalized;
        bullet.GetComponent<Bullet>().Initialize(direction);
    }
}
