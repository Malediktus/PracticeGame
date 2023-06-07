using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldyBagel : Enemy
{
    [Header("Attack")]
    [SerializeField] private float minimumDistanceToHeal;
    [SerializeField] private float healRate;
    [SerializeField] private float healAmount;

    private Health playerHealth;

    protected override void Start()
    {
        base.Start();

        playerHealth = target.GetComponent<Health>();

        InvokeRepeating("Heal", 0, healRate);
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

            if (distance < minimumDistanceToHeal)
            {
                Enemies[i].GetComponent<Health>().Heal(healAmount);
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
