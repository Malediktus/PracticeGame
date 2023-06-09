using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] protected float maxHealth = 50.0f;
    
    public UnityEvent onDeathEvent;
    public UnityEvent onHealthChange;
    protected float health;

    private void Awake() => health = maxHealth;

    public void Damage(float amount)
    {
        health -= amount;
        onHealthChange.Invoke();
        if (health <= 0)
            onDeathEvent.Invoke();
    }

    public void Heal(float amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        onHealthChange.Invoke();
    }

    public float GetMaxHealth() => maxHealth;
    public float GetCurrentHealth() => health;
    public float GetHealthPercent() => health / maxHealth;
}
