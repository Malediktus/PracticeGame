using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    private Rigidbody2D rb;
    private Vector3 targetPosition;

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        
        rb.velocity = ( targetPosition - transform.position ).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.TryGetComponent<Player>(out Player player)) {
            Health health = go.GetComponent<Health>();
            health.Damage(damage);
        }

        Destroy(gameObject);
    }
}
