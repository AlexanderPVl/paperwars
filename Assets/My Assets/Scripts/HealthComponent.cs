using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    private int health;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damagePoints)
    {
        if (health <= 0f)
            Destroy(gameObject);

        health -= damagePoints;
    }
}