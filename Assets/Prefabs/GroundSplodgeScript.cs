using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSplodgeScript : MonoBehaviour
{
    [SerializeField] private float decayRate;
    [SerializeField] private float damagePerSecond;
    [SerializeField] private Player.SpreadType spreadType;
    [SerializeField] private LayerMask enemyLayer;


    // Start is called before the first frame update
    void Start()
    {
        switch (spreadType)
        {
            case Player.SpreadType.Butter:
                InvokeRepeating("DamageEnemies", 0, 1);
                break;
            case Player.SpreadType.Jam:
                InvokeRepeating("JamEnemies", 0, 1);
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localScale -= new Vector3(decayRate * 0.02f, decayRate * 0.02f, decayRate * 0.02f);
    }

    private void DamageEnemies()
    {
        Collider2D[] EnemiesInCircle = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2, enemyLayer);

        for (int i = 0; i < EnemiesInCircle.Length; i++)
        {
            EnemiesInCircle[i].GetComponent<Health>().Damage(damagePerSecond);
        }
    }

    private void JamEnemies()
    {
        Collider2D[] EnemiesInCircle = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2, enemyLayer);

        for (int i = 0; i < EnemiesInCircle.Length; i++)
        {
            EnemiesInCircle[i].GetComponent<Enemy>().Jam();
        }
    }
}
