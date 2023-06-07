using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldyCannonBaguette : Enemy
{
    [Header("Attack")]
    [SerializeField] private float minimumDistanceToAttack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float projectileVelocity;
    [SerializeField] private GameObject projectile;

    private Transform[] cannonPoints = new Transform[6];
    private int shootIndex;

    protected override void Start()
    {
        base.Start();

        // Getting the cannon positions
        for (int i = 0; i < 6; i++)
        {
            cannonPoints[i] = transform.GetChild(1).GetChild(i).transform;
        }
    }

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
        // The function will go through all 6 cannons in order and shoot a shot out of each of them
        Rigidbody2D rb = Instantiate(projectile, cannonPoints[shootIndex].position, Quaternion.identity).GetComponent<Rigidbody2D>();

        rb.velocity = (target.position - transform.position).normalized * projectileVelocity;
        rb.GetComponent<EnemyProjectile>().SetDamage(attackDamage);

        shootIndex++;
        if (shootIndex == 6)
            shootIndex = 0;
    }
}
