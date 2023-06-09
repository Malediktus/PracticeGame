using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] protected float maxHealth = 50.0f;
    [SerializeField] public UnityEvent onDeathEvent;
    [SerializeField] public UnityEvent<float> onDamageEvent;
    [SerializeField] public UnityEvent<float> onHealEvent;

    protected float health;

    private void Awake()
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

        if (health <= 0)
            onDeathEvent.Invoke();
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetHealth()
    {
        return health;
    }
}
