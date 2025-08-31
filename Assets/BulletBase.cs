using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float lifeTime = 3f;

    protected Vector3 direction;
    protected int damage;

    public virtual void Initialize(Vector3 dir, int dmg)
    {
        direction = dir;
        damage = dmg;
        Destroy(gameObject, lifeTime);
    }

    protected virtual void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth health = other.GetComponent<EnemyHealth>();
        if (health == null)
            health = other.GetComponentInParent<EnemyHealth>();

        if (health != null)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}