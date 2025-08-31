using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [Header("Snake Settings")]
    public Transform segmentPrefab; 
    public int initialSize = 3;     
    public float moveDelay = 0.2f;  

    private List<Transform> segments = new List<Transform>();
    private Vector2Int direction = Vector2Int.right; 
    private float timer;
    
    

    void Start()
    {
        segments.Clear();
        segments.Add(this.transform);

        for (int i = 1; i < initialSize; i++)
        {
            Grow();
        }
    }

    void Update()
    {
        HandleInput();

        timer += Time.deltaTime;
        if (timer >= moveDelay)
        {
            timer = 0f;
            Move();
        }
    }

    void HandleInput()
    {
        Vector2Int newDirection = direction;

        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2Int.down)
            newDirection = Vector2Int.up;
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2Int.up)
            newDirection = Vector2Int.down;
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2Int.right)
            newDirection = Vector2Int.left;
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2Int.left)
            newDirection = Vector2Int.right;

        // Проверим, не ведёт ли новый поворот в тело
        Vector3 nextHeadPos = segments[0].position + new Vector3(newDirection.x, newDirection.y, 0);

        bool blocked = false;
        for (int i = 1; i < segments.Count; i++)
        {
            if (segments[i].position == nextHeadPos)
            {
                blocked = true;
                break;
            }
        }

        if (!blocked)
        {
            direction = newDirection; // применяем только если свободно
        }
    }

    void Move()
    {
        Vector3 newPosition = segments[0].position;
        newPosition.x += direction.x;
        newPosition.y += direction.y;

        // Сдвигаем тело
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        // Двигаем голову
        segments[0].position = newPosition;
    }

    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }
}
