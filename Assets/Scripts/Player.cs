using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float speed = 5.0f;

    [Header("Combat")]
    [SerializeField]
    private LayerMask enemyLayer;
    [SerializeField]
    private Transform sword;
    [SerializeField]
    private Transform hitPoint;
    [SerializeField]
    private float swordDamage = 10.0f;
    [SerializeField]
    private float swordCooldown = 0.5f;
    [SerializeField]
    private float swordWidth = 0.0f;
    [SerializeField]
    private float swordHeight = 0.0f;
    [SerializeField]
    private Transform swipeHitPoint;
    [SerializeField]
    private float swipeDamage = 5.0f;
    [SerializeField]
    private float swipeWidth = 0.0f;
    [SerializeField]
    private float swipeHeight = 0.0f;

    private Rigidbody2D rb;
    private Slider healthBar;
    private Health health;
    private SpriteRenderer spriteRenderer;

    private float stabTimeStamp;
    private Vector2 velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = health.GetMaxHealth();
        healthBar.value = health.GetHealth();
        stabTimeStamp = Time.time;
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * speed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        velocity = context.ReadValue<Vector2>();

        if (spriteRenderer.flipX == false && velocity.x == 1.0f)
        {
            spriteRenderer.flipX = true;
            // TODO: I dont realy like this code, maybe the sword shoult be directly in the player sprite and not seperate
            sword.transform.position = new Vector3(sword.transform.position.x + transform.localScale.x * 2, sword.transform.position.y, 0.0f);
        }

        else if (spriteRenderer.flipX == true && velocity.x == -1.0f)
        {
            spriteRenderer.flipX = false;
            sword.transform.position = new Vector3(sword.transform.position.x - transform.localScale.x * 2, sword.transform.position.y, 0.0f);
        }
    }

    public void OnStab(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (Time.time < stabTimeStamp)
            return;

        stabTimeStamp = Time.time + swordCooldown;

        DamageEnemiesInArea(
            new Vector2(hitPoint.position.x, hitPoint.position.y + swordHeight),
            new Vector2(hitPoint.position.x + swordWidth, hitPoint.position.y),
            swordDamage
        );
    }

    public void OnSwipe(InputAction.CallbackContext context)
    {
        // TODO: Start animation if context.started

        if (!context.performed)
            return;

        DamageEnemiesInArea(
            new Vector2(swipeHitPoint.position.x, swipeHitPoint.position.y + swipeHeight),
            new Vector2(swipeHitPoint.position.x + swipeWidth, swipeHitPoint.position.y),
            swipeDamage
        );
    }

    private void DamageEnemiesInArea(Vector2 topRightCorner, Vector2 bottomLeftCorner, float damage)
    {
        var enemies = Physics2D.OverlapAreaAll(topRightCorner, bottomLeftCorner, enemyLayer.value);

        foreach (var enemy in enemies)
            enemy.GetComponent<Health>().Damage(damage);
    }

    public void OnDeath()
    {
        Debug.Log("Player died");
    }

    public void OnDamage(float amount)
    {
        healthBar.value = health.GetHealth();
    }

    public void OnHeal(float amount)
    {
        healthBar.value = health.GetHealth();
    }
}
