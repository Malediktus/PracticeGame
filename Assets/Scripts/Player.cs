using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : GameEntity
{
    [Header("Movement")]
    [SerializeField]
    private float speed = 5.0f;

    private Rigidbody2D rb;
    private Slider healthBar;

    private Vector2 velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * speed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        velocity = context.ReadValue<Vector2>();
    }

    public void OnDeath()
    {
        Debug.Log("Player died");
    }

    public void OnDamage(float amount)
    {
        healthBar.value = health;
    }

    public void OnHeal(float amount)
    {
        healthBar.value = health;
    }
}
