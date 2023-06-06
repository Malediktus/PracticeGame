using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldyCannonBaguette : Enemy
{
    [Header("Attack")]
    [SerializeField] float MinimumDistanceToAttack;
    [SerializeField] float Attack_speed;
    [SerializeField] float Attack_Damage;
    [SerializeField] float Projectile_velocity;
    [SerializeField] GameObject Projectile;

    Transform[] Cannon_points = new Transform[6];
    int ShootIndex;

    protected override void Start()
    {
        base.Start();

        //Getting the cannon positions
        for (int i = 0; i < 6; i++)
        {
            Cannon_points[i] = transform.GetChild(1).GetChild(i).transform;
        }
    }

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
        //The function will go through all 6 cannons in order and shoot a shot out of each of them

        Rigidbody2D rb = Instantiate(Projectile, Cannon_points[ShootIndex].position, Quaternion.identity).GetComponent<Rigidbody2D>();

        rb.velocity = (target.position - transform.position).normalized * Projectile_velocity;
        rb.GetComponent<Enemy_projectile>().SetDamage(Attack_Damage);

        ShootIndex++;

        if (ShootIndex == 6)
        {
            ShootIndex = 0;
        }
    }
}
