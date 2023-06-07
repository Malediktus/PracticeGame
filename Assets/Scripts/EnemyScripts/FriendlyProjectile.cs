using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyProjectile : MonoBehaviour
{
    private float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Health>().Damage(damage);
        }

        Destroy(this.gameObject);
    }

    public void SetDamage(float enemyDamage)
    {
        damage = enemyDamage;
    }
}
