using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [Header("AI")]
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float nextWaypointDistance = 3.0f;

    [Header("Movement")]
    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float speed = 5.0f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Vector2 velocity;

    private Seeker seeker;
    private Rigidbody2D rb;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

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
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }

        reachedEndOfPath = false;
        velocity = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
            currentWaypoint++;
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * speed;
    }
}
