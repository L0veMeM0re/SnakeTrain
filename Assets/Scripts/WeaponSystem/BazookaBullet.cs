using UnityEngine;

public class BazookaBullet : BulletBase
{
    [SerializeField] private float explosionRadius = 3f;

    // Добавляем override и вызываем базовый метод
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // Сначала проверяем, попали ли во врага (базовая логика)
        EnemyHealth health = other.GetComponent<EnemyHealth>();
        if (health == null)
            health = other.GetComponentInParent<EnemyHealth>();

        if (health != null)
        {
            Explode();
            Destroy(gameObject);
        }
    }
    
    // Исправляем сигнатуру метода
    public void Initialize(Vector3 dir, int dmg, float radius = 3f)
    {
        base.Initialize(dir, dmg);
        explosionRadius = radius;
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var col in hits)
        {
            EnemyHealth health = col.GetComponent<EnemyHealth>();
            if (health == null)
                health = col.GetComponentInParent<EnemyHealth>();

            if (health != null)
                health.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}