using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [Header("Snake Settings")]
    public Transform segmentPrefab;          // fallback-префаб, если список ниже пуст
    public int initialSize = 3;              // стартовая длина, если не задан список модулей
    public float moveDelay = 0.2f;           // задержка между «шагами» по сетке

    [Header("Body Setup")]
    public List<Transform> bodyPrefabs;      // порядок модулей тела (каждый — свой префаб оружия/вагона)

    private readonly List<Transform> segments = new List<Transform>();
    private Vector2Int direction = Vector2Int.right;
    private float timer;

    void Start()
    {
        segments.Clear();
        segments.Add(this.transform); // голова — первый сегмент

        if (bodyPrefabs != null && bodyPrefabs.Count > 0)
        {
            // Спавним тело строго по списку модулей из инспектора
            for (int i = 0; i < bodyPrefabs.Count; i++)
            {
                AddSegment(bodyPrefabs[i]);
            }
        }
        else
        {
            // Старое поведение: достраиваем хвост одинаковыми сегментами
            for (int i = 1; i < initialSize; i++)
            {
                AddSegment(segmentPrefab);
            }
        }
    }

    void Update()
    {
        HandleInput();

        timer += Time.deltaTime;
        if (timer >= moveDelay)
        {
            timer = 0f;
            Move(); // движение — как было у тебя
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

        // Проверка: не поворачиваем, если следующая клетка занята телом
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
            direction = newDirection;
        }
    }

    void Move()
    {
        Vector3 newPosition = segments[0].position;
        newPosition.x += direction.x;
        newPosition.y += direction.y;

        // Сдвигаем тело с хвоста к голове (ровно как у тебя было)
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        // Двигаем голову
        segments[0].position = newPosition;
    }

    // Универсальное добавление сегмента заданным префабом
    private void AddSegment(Transform prefab)
    {
        Transform segment = Instantiate(prefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    // Публичный Grow для кода апа уровня: можно добавить конкретный модуль, либо fallback
    public void Grow() => AddSegment(segmentPrefab);
    public void Grow(Transform customPrefab) => AddSegment(customPrefab != null ? customPrefab : segmentPrefab);
}
