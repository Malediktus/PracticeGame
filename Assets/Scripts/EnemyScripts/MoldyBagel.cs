using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldyBagel : Enemy
{
    [Header("Attack")]
    [SerializeField] float MinimumDistanceToHeal;
    [SerializeField] float Heal_rate;
    [SerializeField] float Heal_Amount;

    Health Player_Health;

    protected override void Start()
    {
        base.Start();

        Player_Health = target.GetComponent<Health>();

        InvokeRepeating("Heal", 0, Heal_rate);
    }

    protected override void Update()
    {
        base.Update();
    }

    private void Heal()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float SmallestDistance = Mathf.Infinity;
        int IndexOfSmallest = 0;

        for (int i = 0; i < Enemies.Length; i++)
        {
            float distance = Vector2.Distance(Enemies[i].transform.position, transform.position);

            if (distance < MinimumDistanceToHeal)
            {
                Enemies[i].GetComponent<Health>().Heal(Heal_Amount);
            }

            if (distance < SmallestDistance && distance > 0.1f)
            {
                SmallestDistance = distance;
                IndexOfSmallest = i;
            }
        }

        if (Enemies.Length > 0)
        {
            target = Enemies[IndexOfSmallest].transform;
        }
        else
        {
            target = transform;
        }
    }
}
