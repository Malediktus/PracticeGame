using UnityEngine;

public class MoldyMeleeToast : Enemy
{
    [Header("Attack")]
    [SerializeField] private float attackDamage;

    //private Health playerHealth;

    protected override void Start()
    {
        base.Start();

        // TODO : This is very ugly, need to be changed
        //playerHealth = GetTarget().GetComponent<Health>();
    }

    protected override void Update()
    {
        base.Update();

        // If the enemy is close enough to the player and isn't already invoking, it attacks
        if (DistanceToTarget < minimumDistanceToAttack && !IsInvoking("Attack")) {
            InvokeRepeating("Attack", attackSpeed / 2, attackSpeed);
        } else if (DistanceToTarget > minimumDistanceToAttack) {
            CancelInvoke("Attack");
        }
    }

    private void Attack()
    {
        //playerHealth.Damage(attackDamage);
    }
}
