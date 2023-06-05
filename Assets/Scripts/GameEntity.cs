using UnityEngine;
using UnityEngine.Events;

public class GameEntity : MonoBehaviour
{
    [Header("GameEntity")]
    [SerializeField]
    protected float maxHealth = 50.0f;
    [SerializeField]
    private UnityEvent onDeathEvent;
    [SerializeField]
    private UnityEvent<float> onDamageEvent;
    [SerializeField]
    private UnityEvent<float> onHealEvent;

    protected float health;

    public GameEntity()
    {
        health = maxHealth;
    }

    public void Damage(float amount)
    {
        health -= amount;
        onDamageEvent.Invoke(amount);
        if (health <= 0)
            onDeathEvent.Invoke();
    }

    public void Heal(float amount)
    {
        health += amount;
        onHealEvent.Invoke(amount);
        if (health > maxHealth)
            health = maxHealth;
    }
}
