using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldyMeleeToast : Enemy
{
    [Header("Attack")]
    [SerializeField] float MinimumDistanceToAttack;
    [SerializeField] float Attack_speed;
    [SerializeField] float Attack_Damage;

    GameEntity Player_Entity;

    protected override void Start()
    {
        base.Start();

        Player_Entity = GameObject.Find("Player").GetComponent<GameEntity>();
    }

    protected override void Update()
    {
        base.Update();

        //Gets distance between itself and player
        float distance = Vector2.Distance(rb.position, target.position);

        //if the enemy is close enough to the player and isn't already invoking, it attacks
        if (distance < MinimumDistanceToAttack && !IsInvoking("Attack"))
        {
            InvokeRepeating("Attack", Attack_speed, Attack_speed);
        }
        else if(distance > MinimumDistanceToAttack)
        {
            CancelInvoke("Attack");
        }
    }

    private void Attack()
    {
        Player_Entity.Damage(Attack_Damage);
    }
}
