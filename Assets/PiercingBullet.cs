using UnityEngine;

public class PiercingBullet : BulletBase
{
    [SerializeField] private int maxPierceCount = 3;
    private int piercedCount = 0;

    public void Initialize(Vector3 dir, int dmg, int pierceCount)
    {
        base.Initialize(dir, dmg);
        maxPierceCount = pierceCount;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth health = other.GetComponent<EnemyHealth>();
        if (health == null)
            health = other.GetComponentInParent<EnemyHealth>();

        if (health != null)
        {
            health.TakeDamage(damage);
            piercedCount++;
            if (piercedCount >= maxPierceCount)
                Destroy(gameObject);
        }
    }
}