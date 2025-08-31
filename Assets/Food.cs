using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Snake2D snake))
        {
            snake.Grow(); // змейка растёт
            // перемещаем еду в случайное место
            transform.position = new Vector3(
                Random.Range(-8, 8),
                Random.Range(-4, 4),
                0
            );
        }
    }
}