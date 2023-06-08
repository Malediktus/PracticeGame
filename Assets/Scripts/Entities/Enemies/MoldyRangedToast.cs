using UnityEngine;

public class MoldyRangedToast : RangedEnemy
{
    //[SerializeField] private float attackDamage;
    //[SerializeField] private float projectileVelocity;
    //[SerializeField] private GameObject projectile;

    protected override void Update()
    {
        base.Update();

        /*// Gets distance between itself and player
        float distance = Vector2.Distance(rb.position, GetTarget().position);

        // If the enemy is close enough to the player and isn't already invoking, it attacks
        if (distance < minimumDistanceToAttack && !IsInvoking("Shoot"))
        {
            InvokeRepeating("Shoot", 1, attackSpeed);
        }
        else if(distance > minimumDistanceToAttack)
        {
            CancelInvoke("Shoot");
        }*/

        // If the enemy is close enough to the player and isn't already invoking, it attacks
        if (DistanceToTarget < minimumDistanceToAttack && !IsInvoking("Shoot"))
        {
            InvokeRepeating("Shoot", 1, attackSpeed);
        }
        else if(DistanceToTarget > minimumDistanceToAttack)
        {
            CancelInvoke("Shoot");
        }
    }

    protected override void Shoot()
    {
        /*Rigidbody2D rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();

        rb.velocity = (GetTarget().position - transform.position).normalized * projectileVelocity;
        rb.GetComponent<EnemyProjectile>().SetDamage(attackDamage);*/

        CreateProjectile(transform.position, targetPosition.Value, out GameObject projectile);
    }
}
