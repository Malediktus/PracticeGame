using UnityEngine;

public abstract class RangedEnemy : Enemy
{
    [SerializeField] private EnemyProjectile projectilePrefab; // I specify the type of the prefab to be sure to have the correct one

    protected override void Update() {
        base.Update();

        if (DistanceToTarget < minimumDistanceToAttack && !IsInvoking("Shoot")) {
            InvokeRepeating("Shoot", 1, attackSpeed);
        } else if (DistanceToTarget > minimumDistanceToAttack) {
            CancelInvoke("Shoot");
        }
    }

    protected abstract void Shoot();

    protected void CreateProjectile( Vector3 origin, Vector3 targetPosition, out GameObject go ) {
        go = Instantiate(projectilePrefab.gameObject, origin, Quaternion.identity);
        EnemyProjectile projectile = go.GetComponent<EnemyProjectile>();
        projectile.SetTargetPosition(targetPosition);
    }
}
