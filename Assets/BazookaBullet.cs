using UnityEngine;

public class BazookaBullet : BulletBase
{
    [SerializeField] private float explosionRadius = 3f;
    private Vector3 target;


    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // Взрыв при контакте с врагом или любым коллайдером (можно добавить слой)
        Explode();
    }
    
    public void Initialize(Vector3 dir, int dmg, float radius)
    {
        base.Initialize(dir, dmg);
        explosionRadius = radius;
        target = transform.position + dir; 
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