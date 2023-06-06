using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldyMeleeToast : Enemy
{
    [Header("Attack")]
    [SerializeField] float MinimumDistanceToAttack;
    [SerializeField] float Attack_speed;
    [SerializeField] float Attack_Damage;

    Health Player_Health;

    protected override void Start()
    {
        base.Start();

        Player_Health = target.GetComponent<Health>();
    }

    protected override void Update()
    {
        base.Update();

        //Gets distance between itself and player
        float distance = Vector2.Distance(rb.position, target.position);

        //if the enemy is close enough to the player and isn't already invoking, it attacks
        if (distance < MinimumDistanceToAttack && !IsInvoking("Attack"))
        {
            InvokeRepeating("Attack", Attack_speed / 2, Attack_speed);
        }
        else if(distance > MinimumDistanceToAttack)
        {
            CancelInvoke("Attack");
        }
    }

    private void Attack()
    {
        Player_Health.Damage(Attack_Damage);
    }
}
