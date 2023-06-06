using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [Header("AI")]
    [SerializeField] private Transform target;
    [SerializeField] private float nextWaypointDistance = 3.0f;
    [SerializeField] private float stopDistanceFromTarget = 0.5f;

    [Header("Movement")]
    [SerializeField] private float speed = 5.0f;

    private Path path;
    private int currentWaypoint = 0;

    private Vector2 velocity;

    private Seeker seeker;
    private Rigidbody2D rb;
    private Slider healthBar;
    private Health health;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = health.GetMaxHealth();
        healthBar.value = health.GetHealth();

        InvokeRepeating("UpdatePath", 0.0f, 0.5f);
    }

    private void UpdatePath()
    {
        if (!seeker.IsDone())
            return;

        seeker.StartPath(transform.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (p.error)
            return;

        path = p;
        currentWaypoint = 0;
    }

    private void Update()
    {
        if (path == null || currentWaypoint >= path.vectorPath.Count)
            return;

        if (Vector2.Distance(transform.position, target.position) <= stopDistanceFromTarget)
            velocity = Vector2.zero;
        else
            velocity = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
            currentWaypoint++;
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * speed;
    }

    public void OnDeath()
    {
        Destroy(gameObject);
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
