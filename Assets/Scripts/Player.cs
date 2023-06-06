using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float speed = 5.0f;

    [Header("Combat")]
    [SerializeField]
    private float swordWidth = 0.0f;
    [SerializeField]
    private float swordHeight = 0.0f;
    [SerializeField]
    private Transform sword;
    [SerializeField]
    private Transform hitPoint;
    [SerializeField]
    private LayerMask enemyLayer;
    [SerializeField]
    private float swordStapDamage = 10.0f;
    [SerializeField]
    private float swordStapCooldown = 0.5f;

    private Rigidbody2D rb;
    private Slider healthBar;
    private Health health;
    private SpriteRenderer spriteRenderer;
    private float timeStamp;

    private Vector2 velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = health.GetMaxHealth();
        healthBar.value = health.GetHealth();
        timeStamp = Time.time;
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * speed;
    }

    private void OnDrawGizmos()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(0, 0, 0),
            new Vector3(swordWidth, 0, 0),
            new Vector3(0, swordHeight, 0),
            new Vector3(swordWidth, swordHeight, 0)
        };
        mesh.vertices = vertices;

        int[] tris = new int[6]
        {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
        };
        mesh.triangles = tris;

        Vector3[] normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
        mesh.normals = normals;

        Vector2[] uv = new Vector2[4]
        {
              new Vector2(0, 0),
              new Vector2(1, 0),
              new Vector2(0, 1),
              new Vector2(1, 1)
        };
        mesh.uv = uv;

        Gizmos.DrawWireMesh(mesh, 0, hitPoint.position);
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

    public void OnFire(InputAction.CallbackContext context)
    {
        if (Time.time < timeStamp)
            return;

        timeStamp = Time.time + swordStapCooldown;

        var enemies = Physics2D.OverlapAreaAll(
            new Vector2(hitPoint.position.x, hitPoint.position.y + swordHeight),
            new Vector2(hitPoint.position.x + swordWidth, hitPoint.position.y),
            enemyLayer.value
        );

        foreach(var enemy in enemies)
        {
            enemy.GetComponent<Health>().Damage(swordStapDamage);
        }
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
