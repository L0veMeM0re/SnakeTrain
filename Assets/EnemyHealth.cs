using UnityEngine;

public class EnemyHealth : Health
{
    public GameObject experiencePrefab;  // префаб опыта

    protected override void Die()
    {
        SpawnExperience();
        Destroy(gameObject);
    }

    private void SpawnExperience()
    {
        if (experiencePrefab != null)
        {
            Instantiate(experiencePrefab, transform.position, Quaternion.identity);
        }
    }
}