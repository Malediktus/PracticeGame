using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldyRangedToast : Enemy
{
    [Header("Attack")]
    [SerializeField] private float minimumDistanceToAttack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float projectileVelocity;
    [SerializeField] private GameObject projectile;

    protected override void Update()
    {
        base.Update();

        // Gets distance between itself and player
        float distance = Vector2.Distance(rb.position, target.position);

        // If the enemy is close enough to the player and isn't already invoking, it attacks
        if (distance < minimumDistanceToAttack && !IsInvoking("Shoot"))
        {
            InvokeRepeating("Shoot", 1, attackSpeed);
        }
        else if(distance > minimumDistanceToAttack)
        {
            CancelInvoke("Shoot");
        }
    }

    private void Shoot()
    {
        Rigidbody2D rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();

        rb.velocity = (target.position - transform.position).normalized * projectileVelocity;
        rb.GetComponent<EnemyProjectile>().SetDamage(attackDamage);
    }
}
