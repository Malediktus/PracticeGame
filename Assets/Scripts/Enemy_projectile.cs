using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_projectile : MonoBehaviour
{
    float Damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            collision.gameObject.GetComponent<GameEntity>().Damage(Damage);
        }

        Destroy(this.gameObject);
    }

    public void SetDamage(float Enemy_Damage)
    {
        Damage = Enemy_Damage;
    }
}
