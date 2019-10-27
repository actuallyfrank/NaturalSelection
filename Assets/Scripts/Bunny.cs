using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bunny : Animal
{
    private Animator animator;

    protected override void OnEnable()
    {
        base.OnEnable();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("Speed", NavMeshAgent.velocity.magnitude / NavMeshAgent.speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Food food = other.GetComponent<Food>();

        if (food == null) return;
        IncreaseEnergy(food.Eat());
    }
}
