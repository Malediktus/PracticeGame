using UnityEngine;

public abstract class RangedEnemy : OffenseEnemy
{
    [SerializeField] private EnemyProjectile projectilePrefab; // I specify the type of the prefab to be sure to have the correct one

    protected void CreateProjectile( Vector3 origin, Vector3 targetPosition, out GameObject go ) {
        go = Instantiate(projectilePrefab.gameObject, origin, Quaternion.identity);
        EnemyProjectile projectile = go.GetComponent<EnemyProjectile>();
        projectile.SetTargetPosition(targetPosition);
    }
}
