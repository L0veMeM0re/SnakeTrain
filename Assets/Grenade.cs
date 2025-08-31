using UnityEngine;

public class GrenadeBullet : BulletBase
{
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float fuseTime = 1.5f;
    private Vector3 target;

    public void Initialize(Vector3 dir, int dmg, float radius)
    {
        base.Initialize(dir, dmg);
        explosionRadius = radius;
        target = transform.position + dir; // цель, куда летим
        Invoke(nameof(Explode), fuseTime);
    }

    protected override void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
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
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}