using UnityEngine;
using UnityEngine.Events;

public class GameEntity : MonoBehaviour
{
    [Header("GameEntity")]
    [SerializeField]
    private float maxHealth = 50.0f;
    [SerializeField]
    private UnityEvent onDeathEvent;

    private float health;

    public GameEntity()
    {
        health = maxHealth;
    }

    public void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
            onDeathEvent.Invoke();
    }

    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth)
            health = maxHealth;
    }
}
