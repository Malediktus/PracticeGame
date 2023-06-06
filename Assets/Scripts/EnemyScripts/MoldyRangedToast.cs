using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldyRangedToast : Enemy
{
    [Header("Attack")]
    [SerializeField] float MinimumDistanceToAttack;
    [SerializeField] float Attack_speed;
    [SerializeField] float Attack_Damage;
    [SerializeField] float Projectile_velocity;
    [SerializeField] GameObject Projectile;

    protected override void Update()
    {
        base.Update();

        //Gets distance between itself and player
        float distance = Vector2.Distance(rb.position, target.position);

        //if the enemy is close enough to the player and isn't already invoking, it attacks
        if (distance < MinimumDistanceToAttack && !IsInvoking("Shoot"))
        {
            InvokeRepeating("Shoot", 1, Attack_speed);
        }
        else if(distance > MinimumDistanceToAttack)
        {
            CancelInvoke("Shoot");
        }
    }

    private void Shoot()
    {
        Rigidbody2D rb = Instantiate(Projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();

        rb.velocity = (target.position - transform.position).normalized * Projectile_velocity;
        rb.GetComponent<Enemy_projectile>().SetDamage(Attack_Damage);
    }
}
