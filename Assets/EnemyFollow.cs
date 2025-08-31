using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyFollow : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float moveSpeed = 2f;         // скорость движения
    public float stopDistance = 0.5f;    // дистанция остановки перед целью
    public int damage = 10;              // урон игроку
    public float attackCooldown = 1f;    // время между атаками

    private Transform target;            // цель (голова змейки)
    private PlayerHealth playerHealth;   // ссылка на ХП игрока
    private float lastAttackTime;        // таймер для атаки
    private Rigidbody2D rb;              // Rigidbody2D

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Находим игрока по тегу
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // Расстояние до цели
        float distance = Vector2.Distance(transform.position, target.position);

        // Двигаемся к цели, если не слишком близко
        if (distance > stopDistance)
        {
            Vector2 direction = (target.position - transform.position).normalized;

            // Двигаемся с помощью Rigidbody2D, чтобы учитывать столкновения
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Атака игрока при близком расстоянии
            if (Time.time >= lastAttackTime + attackCooldown && playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }
}