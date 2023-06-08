using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldyMeleeToast : Enemy
{
    [Header("Attack")]
    [SerializeField] private float minimumDistanceToAttack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDamage;

    private Health playerHealth;
    private AudioSource runSound;

    protected override void Start()
    {
        base.Start();
    
    playerHealth = GetTarget().GetComponent<Health>();
    runSound = GetComponent<AudioSource>();

    }

    protected override void Update()
    {
        base.Update();

        // Gets distance between itself and player
        float distance = Vector2.Distance(rb.position, GetTarget().position);

        // If the enemy is close enough to the player and isn't already invoking, it attacks
        if (distance < minimumDistanceToAttack && !IsInvoking("Attack"))
        {
            InvokeRepeating("Attack", attackSpeed / 2, attackSpeed);
        }
        else if(distance > minimumDistanceToAttack)
        {
            CancelInvoke("Attack");
        }
    }

    private void Attack()
    {
        playerHealth = GetTarget().GetComponent<Health>();
        playerHealth.Damage(attackDamage);
        runSound.Play();
    }
}
