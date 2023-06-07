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
    [SerializeField] private float jammedSpeedMultiplier;

    private Path path;
    private int currentWaypoint = 0;

    private Vector2 velocity;

    private Seeker seeker;
    protected Rigidbody2D rb;
    private Slider healthBar;
    private Health health;

    private float currentSpeed;
    protected bool IsJammed;
    protected bool IsCured;

    protected virtual void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;

        health = GetComponent<Health>();
        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = health.GetMaxHealth();
        healthBar.value = health.GetHealth();

        currentSpeed = speed;

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

    protected virtual void Update()
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

    public Transform GetTarget()
    {
        return target;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void Jam()
    {
        CancelInvoke("UnJam");

        if (!IsJammed)
        {
            IsJammed = true;
            currentSpeed *= jammedSpeedMultiplier;
        }

        Invoke("UnJam", 5);
    }

    private void UnJam()
    {
        IsJammed = false;
        currentSpeed /= jammedSpeedMultiplier;
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * currentSpeed;
    }

    public void Cure()
    {
        LookForEnemyTarget();
        gameObject.layer = 10;
        IsCured = true;

        Invoke("Reinfect", 20);
    }

    private void LookForEnemyTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float smallestDistance = Mathf.Infinity;
        int indexOfSmallest = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            float distance = Vector2.Distance(enemies[i].transform.position, transform.position);

            if (distance < smallestDistance && distance > 0.1f)
            {
                smallestDistance = distance;
                indexOfSmallest = i;
            }
        }

        if (enemies.Length > 0)
        {
            SetTarget(enemies[indexOfSmallest].transform);
            GetTarget().gameObject.GetComponent<Health>().onDeathEvent.AddListener(OnTargetDeath);
        }
        else
        {
            GetTarget().gameObject.GetComponent<Health>().onDeathEvent.RemoveListener(OnTargetDeath);
            SetTarget(transform);
        }
    }
    public void OnTargetDeath()
    {
        GetTarget().gameObject.GetComponent<Health>().onDeathEvent.RemoveListener(OnTargetDeath);
        SetTarget(transform);
    }

    private void Reinfect()
    {
        target = GameObject.Find("Player").transform;
        gameObject.layer = 6;

        IsCured = false;
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
