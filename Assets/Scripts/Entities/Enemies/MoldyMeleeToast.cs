using UnityEngine;

public class MoldyMeleeToast : OffenseEnemy
{
    [SerializeField] private float attackDamage;

    //private Health playerHealth;

    protected override void Start()
    {
        base.Start();

        // TODO : This is very ugly, need to be changed
        //playerHealth = GetTarget().GetComponent<Health>();
    }

    protected override void Attack() {
        //playerHealth.Damage(attackDamage);
    }
}
