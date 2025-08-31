using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public Snake snake; 
    public int currentLevel = 1;              // Текущий уровень игрока
    public int currentExperience = 0;         // Текущий опыт
    public int experienceToNextLevel = 10;    // Сколько нужно опыта для следующего уровня

    // Добавление опыта
    public void AddExperience(int amount)
    {
        currentExperience += amount;
        Debug.Log($"Получено опыта: {amount} | Всего: {currentExperience}/{experienceToNextLevel}");

        CheckLevelUp();
    }

    // Проверка на ап уровня
    private void CheckLevelUp()
    {
        while (currentExperience >= experienceToNextLevel)
        {
            currentExperience -= experienceToNextLevel;
            currentLevel++;

            // Можно усложнить формулу роста
            experienceToNextLevel = Mathf.RoundToInt(experienceToNextLevel * 1.5f);

            Debug.Log($"УРОВЕНЬ ПОВЫШЕН! Новый уровень: {currentLevel}");
        }
    }
    
    
}