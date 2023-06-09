using UnityEngine;
using System.Collections.Generic;

public class MoldyCannonBaguette : RangedEnemy
{
    [Header("Attack")]
    [SerializeField] private List<Transform> cannonPoints = new List<Transform>();

    private int shootIndex;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        CreateProjectile(cannonPoints[shootIndex].position, targetPosition.Value, out GameObject projectile);

        shootIndex++;
        if (shootIndex == 6)
            shootIndex = 0;
    }
}
