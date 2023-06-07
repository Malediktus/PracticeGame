using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IPowerable
{
    [Header("Movement")]
    [SerializeField] private float speed = 4.0f;

    [Header("Combat")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform sword;
    [SerializeField] private Transform hitPoint;
    [SerializeField] private float swordDamage = 10.0f;
    [SerializeField] private float swordCooldown = 0.5f;
    [SerializeField] private float swordWidth = 0.0f;
    [SerializeField] private float swordHeight = 0.0f;
    [SerializeField] private Transform swipeHitPoint;
    [SerializeField] private float swipeDamage = 5.0f;
    [SerializeField] private float swipeWidth = 0.0f;
    [SerializeField] private float swipeHeight = 0.0f;

    [Header("Ranged Combat")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float smallButterShotDamage = 10.0f;
    [SerializeField] private float smallButterConsumption = 1f;
    [SerializeField] private float butterCapacity = 100f;
    [SerializeField] private float smallJamConsumption = 1f;
    [SerializeField] private float jamCapacity = 100f;
    [SerializeField] private float projectileVelocity = 10.0f;
    [SerializeField] private float blasterCooldown = 0.5f;

    [Header("Death screen")]
    [SerializeField] private GameObject deathScreen;

    [Header("Spread bars")]
    [SerializeField] private Slider butterBar;
    [SerializeField] private Slider jamBar;
    [SerializeField] private GameObject antidoteSlot;

    private Rigidbody2D rb;
    private Slider healthBar;
    private Health health;
    private SpriteRenderer spriteRenderer;

    private float shootTimeStamp;
    private float stabTimeStamp;
    private Vector2 velocity;

    private float currentButterAmount;
    private float currentJamAmount;
    private bool hasAntidote = true;

    private SpreadType spreadType;

    public enum SpreadType
    {
        Butter,
        Jam,
        Antidote
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = health.GetMaxHealth();
        healthBar.value = health.GetHealth();
        stabTimeStamp = Time.time;

        currentButterAmount = butterCapacity;
        currentJamAmount = jamCapacity;
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * speed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        velocity = context.ReadValue<Vector2>();

        if (spriteRenderer.flipX == false && velocity.x > 0)
        {
            spriteRenderer.flipX = true;
            // TODO: I dont realy like this code, maybe the sword shoult be directly in the player sprite and not seperate
            sword.transform.position = new Vector3(sword.transform.position.x + transform.localScale.x * 2, sword.transform.position.y, 0.0f);
        }

        else if (spriteRenderer.flipX == true && velocity.x < 0)
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

    public void OnSpreadSwitch(InputAction.CallbackContext context)
    {
        // TODO: Start animation if context.started

        if (!context.performed)
            return;

        spreadType += 1;

        if ((int)spreadType > 2)
        {
            spreadType = 0;
        }
    }

    public void OnSmallShoot(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (Time.time < shootTimeStamp)
            return;

        if (spreadType == SpreadType.Butter)
        {
            if (0 >= currentButterAmount - smallButterConsumption)
                return;

            shootTimeStamp = Time.time + blasterCooldown;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Rigidbody2D projectileRigidBody = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            projectileRigidBody.velocity = (mousePos - (Vector2)transform.position).normalized * projectileVelocity;

            PlayerProjectile currentProjectile = projectileRigidBody.GetComponent<PlayerProjectile>();

            currentProjectile.SetDamage(smallButterShotDamage);
            currentProjectile.SetSpreadType(spreadType);
            currentButterAmount -= smallButterConsumption;

            butterBar.value = currentButterAmount / butterCapacity;
        }
        else if (spreadType == SpreadType.Jam)
        {
            if (0 >= currentJamAmount - smallJamConsumption)
                return;

            shootTimeStamp = Time.time + blasterCooldown;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Rigidbody2D projectileRigidBody = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            projectileRigidBody.velocity = (mousePos - (Vector2)transform.position).normalized * projectileVelocity;

            PlayerProjectile currentProjectile = projectileRigidBody.GetComponent<PlayerProjectile>();

            currentProjectile.SetSpreadType(spreadType);
            currentJamAmount -= smallJamConsumption;

            jamBar.value = currentJamAmount / jamCapacity;
        }
        else 
        {
            if (!hasAntidote)
                return;

            shootTimeStamp = Time.time + blasterCooldown;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Rigidbody2D projectileRigidBody = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            projectileRigidBody.velocity = (mousePos - (Vector2)transform.position).normalized * projectileVelocity;

            PlayerProjectile currentProjectile = projectileRigidBody.GetComponent<PlayerProjectile>();

            currentProjectile.SetSpreadType(spreadType);
            hasAntidote = false;

            antidoteSlot.SetActive(false);
        }
    }

    private void DamageEnemiesInArea(Vector2 topRightCorner, Vector2 bottomLeftCorner, float damage)
    {
        var enemies = Physics2D.OverlapAreaAll(topRightCorner, bottomLeftCorner, enemyLayer.value);

        foreach (var enemy in enemies)
            enemy.GetComponent<Health>().Damage(damage);
    }

    public void OnDeath()
    {
        Time.timeScale = 0;
        deathScreen.SetActive(true);
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
