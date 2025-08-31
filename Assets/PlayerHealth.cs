using UnityEngine;

public class PlayerHealth : Health
{
    protected override void Die()
    {
        Debug.Log("Игрок умер! Показываем экран Game Over.");
        // Тут можно вызвать UI смерти или рестарт сцены
    }
}