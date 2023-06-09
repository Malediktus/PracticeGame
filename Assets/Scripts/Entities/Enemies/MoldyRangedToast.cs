using UnityEngine;

public class MoldyRangedToast : RangedEnemy
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        CreateProjectile(transform.position, targetPosition.Value, out GameObject projectile);
    }
}
