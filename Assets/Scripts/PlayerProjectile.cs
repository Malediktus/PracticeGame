using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private float damage;
    private Player.SpreadType spreadType;

    [SerializeField] private Color Butter;
    [SerializeField] private Color Jam;
    [SerializeField] private Color Antidote;

    [SerializeField] private GameObject butterSplodge;
    [SerializeField] private GameObject jamSplodge;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        switch (spreadType)
        {
            case Player.SpreadType.Butter:
                sr.color = Butter;
                break;
            case Player.SpreadType.Jam:
                sr.color = Jam;
                break;
            case Player.SpreadType.Antidote:
                sr.color = Antidote;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            switch (spreadType)
            {
                case Player.SpreadType.Butter:
                    collision.gameObject.GetComponent<Health>().Damage(damage);
                    if (transform.localScale.x > 1)
                        Instantiate(butterSplodge, transform.position, Quaternion.identity);
                    break;
                case Player.SpreadType.Jam:
                    collision.gameObject.GetComponent<Enemy>().Jam();
                    if (transform.localScale.x > 1)
                        Instantiate(jamSplodge, transform.position, Quaternion.identity);
                    break;
                case Player.SpreadType.Antidote:
                    collision.gameObject.GetComponent<Enemy>().Cure();
                    break;
            }
        }

        Destroy(this.gameObject);
    }

    public void SetDamage(float Damage)
    {
        damage = Damage;
    }

    public void SetSpreadType(Player.SpreadType currentSpread)
    {
        spreadType = currentSpread;
    }
}
