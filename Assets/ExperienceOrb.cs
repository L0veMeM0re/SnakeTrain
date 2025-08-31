using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    public int experienceAmount = 10;  // сколько опыта даёт этот объект

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что это голова змейки
        if (other.CompareTag("Player"))
        {
            PlayerExperience playerExp = other.GetComponent<PlayerExperience>();
            if (playerExp != null)
            {
                playerExp.AddExperience(experienceAmount);
            }

            // уничтожаем объект опыта после сбора
            Destroy(gameObject);
        }
    }
}