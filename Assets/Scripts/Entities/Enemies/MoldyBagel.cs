using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldyBagel : Enemy
{
    [Header("Attack")]
    [SerializeField] private float minimumDistanceToHeal;
    [SerializeField] private float healRate;
    [SerializeField] private float healAmount;

    protected override void Start()
    {
        base.Start();
        InvokeRepeating("Heal", 0, healRate);
    }

    protected override void Update()
    {
        base.Update();
    }

    private void Heal()
    {
        // TODO: Use a circle cast
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float smallestDistance = Mathf.Infinity;
        int indexOfSmallest = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            float distance = Vector2.Distance(enemies[i].transform.position, transform.position);

            if (distance < minimumDistanceToHeal)
            {
                enemies[i].GetComponent<Health>().Heal(healAmount);
            }

            if (distance < smallestDistance && distance > 0.1f)
            {
                smallestDistance = distance;
                indexOfSmallest = i;
            }
        }

        if (enemies.Length > 0)
        {
            SetTarget(enemies[indexOfSmallest].transform);
            GetTarget().gameObject.GetComponent<Health>().onDeathEvent.AddListener(OnTargetDeath);
        }
        else
        {
            GetTarget().gameObject.GetComponent<Health>().onDeathEvent.RemoveListener(OnTargetDeath);
            SetTarget(transform);
        }
    }

    public void OnTargetDeath()
    {
        GetTarget().gameObject.GetComponent<Health>().onDeathEvent.RemoveListener(OnTargetDeath);
        SetTarget(transform);
    }
}
