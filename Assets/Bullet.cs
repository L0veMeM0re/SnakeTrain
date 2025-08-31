using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifeTime = 3f;

    private Vector3 direction;

    public void Initialize(Vector3 dir)
    {
        direction = dir;
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
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