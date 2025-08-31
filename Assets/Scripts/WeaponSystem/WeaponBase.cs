using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected float fireRate = 1f;
    [SerializeField] protected float detectionRange = 10f;
    [SerializeField] protected int damage = 10;

    protected float fireCooldown;

    protected virtual void Update()
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

    /// <summary> Метод стрельбы у каждого оружия свой </summary>
    protected abstract void Shoot(Vector3 targetPosition);

    /// <summary> Поиск ближайшего врага </summary>
    protected Transform FindClosestEnemy()
    {
        EnemyFollow[] enemies = FindObjectsOfType<EnemyFollow>();
        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDistance && dist <= detectionRange)
            {
                closestDistance = dist;
                closest = enemy.transform;
            }
        }

        return closest;
    }
}