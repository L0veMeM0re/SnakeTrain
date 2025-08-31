using System.Collections.Generic;
using UnityEngine;

public class Snake2D : MonoBehaviour
{
    [Header("Настройки движения")]
    public float moveRate = 0.2f; // время между шагами
    private float timer;

    [Header("Сегменты змейки")]
    public Transform segmentPrefab; // префаб вагончика
    private List<Transform> segments = new List<Transform>();

    private Vector2Int direction = Vector2Int.right; // стартовое направление

    void Start()
    {
        segments.Add(transform); // добавляем голову в список
    }

    void Update()
    {
        HandleInput();

        timer += Time.deltaTime;
        if (timer >= moveRate)
        {
            Move();
            timer = 0f;
        }
    }

    void HandleInput()
    {
        // управление направлением (WASD или стрелки)
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2Int.down)
            direction = Vector2Int.up;
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2Int.up)
            direction = Vector2Int.down;
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2Int.right)
            direction = Vector2Int.left;
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2Int.left)
            direction = Vector2Int.right;
    }

    void Move()
    {
        // двигаем тело: каждый сегмент встаёт на позицию предыдущего
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        // двигаем голову вперёд
        Vector3 pos = segments[0].position;
        pos.x += direction.x;
        pos.y += direction.y;
        segments[0].position = pos;
    }

    public void Grow()
    {
        // создаём новый сегмент
        Transform newSegment = Instantiate(segmentPrefab);
        // ставим его в конец (на позицию хвоста)
        newSegment.position = segments[segments.Count - 1].position;
        // добавляем в список
        segments.Add(newSegment);
    }
}