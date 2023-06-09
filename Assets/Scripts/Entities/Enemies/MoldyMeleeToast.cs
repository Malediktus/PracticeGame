using UnityEngine;

public class MoldyMeleeToast : OffenseEnemy
{
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private float attackDamage;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, TargetDirection, minimumDistanceToAttack, attackLayer);
        Debug.Log(hit.collider.name == name);
        if (hit.collider == null) return;

        GameObject go = hit.collider.gameObject;

        if (go.TryGetComponent(out Player player)) {

            Health health = player.GetComponent<Health>();
            health.Damage(attackDamage);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + TargetDirection * minimumDistanceToAttack);
    }
}
