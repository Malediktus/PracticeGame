using UnityEngine;
using System.Collections.Generic;

public class MoldyCannonBaguette : RangedEnemy
{
    //[SerializeField] private float attackDamage;
    //[SerializeField] private float projectileVelocity;
    //[SerializeField] private GameObject projectile;
    [Header("Attack")]
    [SerializeField] private List<Transform> cannonPoints = new List<Transform>();

    //private Transform[] cannonPoints = new Transform[6];
    private int shootIndex;

    protected override void Start()
    {
        base.Start();

        // Getting the cannon positions
        /*for (int i = 0; i < 6; i++)
        {
            cannonPoints[i] = transform.GetChild(1).GetChild(i).transform;
        }*/
    }

    protected override void Update()
    {
        base.Update();

        /*// Gets distance between itself and player
        float distance = Vector2.Distance(rb.position, GetTarget().position);

        // If the enemy is close enough to the player and isn't already invoking, it attacks
        if (distance < minimumDistanceToAttack && !IsInvoking("Shoot")) {
            InvokeRepeating("Shoot", 1, attackSpeed);
        } else if (distance > minimumDistanceToAttack) {
            CancelInvoke("Shoot");
        }*/

        // If the enemy is close enough to the player and isn't already invoking, it attacks
        /*if (DistanceToTarget < minimumDistanceToAttack && !IsInvoking("Shoot")) {
            InvokeRepeating("Shoot", 1, attackSpeed);
        } else if (DistanceToTarget > minimumDistanceToAttack) {
            CancelInvoke("Shoot");
        }*/
    }

    protected override void Shoot()
    {
        // The function will go through all 6 cannons in order and shoot a shot out of each of them
        /*Rigidbody2D rb = Instantiate(projectile, cannonPoints[shootIndex].position, Quaternion.identity).GetComponent<Rigidbody2D>();

        //rb.velocity = (GetTarget().position - transform.position).normalized * projectileVelocity;
        rb.velocity = (targetPosition.Value - transform.position).normalized * projectileVelocity;
        rb.GetComponent<EnemyProjectile>().SetDamage(attackDamage);*/

        CreateProjectile(cannonPoints[shootIndex].position, targetPosition.Value, out GameObject projectile);

        shootIndex++;
        if (shootIndex == 6)
            shootIndex = 0;
    }
}
