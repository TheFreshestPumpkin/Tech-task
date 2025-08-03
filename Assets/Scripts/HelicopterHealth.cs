using UnityEngine;

public class HelicopterHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    private EnemyHelicopter enemyScript;

    private void Start()
    {
        currentHealth = maxHealth;
        enemyScript = GetComponent<EnemyHelicopter>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (enemyScript != null)
        {
            enemyScript.DestroyHelicopter();
        }
    }
}