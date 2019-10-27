using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public partial class Animal : MonoBehaviour, IAnimal
{
    public float EnergyLevel = 100;
    [Range(0.3f, 2f)]
    public float MovementSpeed = 1;

    public NavMeshAgent NavMeshAgent { get; private set; }
    public FieldOfView FieldOfView { get; private set; }

    protected virtual void OnEnable()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        FieldOfView = GetComponent<FieldOfView>();

        StartCoroutine(UpdateBehaviourWithDelay(1f));
    }

    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    protected IEnumerator UpdateBehaviourWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            UpdateBehaviour();
        }
    }

    protected virtual void UpdateBehaviour()
    {
        if (FieldOfView.visibleTargets.Count > 0 && Fertile)
        {
            Transform target = FieldOfView.visibleTargets[0].transform;
            SetNewTargetLocation(Vector3.MoveTowards(transform.position, target.position, MovementSpeed));
            AttemptMatingWithTarget(target.gameObject);
        }
        else
        {
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * MovementSpeed;
            if (randomOffset.magnitude > 0.2f)
                SetNewTargetLocation(transform.position + randomOffset);
        }        
    }

    protected virtual void SetNewTargetLocation(Vector3 targetLocation)
    {
        if (NavMeshAgent != null)
            NavMeshAgent.SetDestination(targetLocation);
        DecreaseEnergy(Vector3.Distance(transform.position, targetLocation));
    }

    protected virtual void IncreaseEnergy(float amount)
    {
        EnergyLevel += amount;
        if (EnergyLevel > 100)
            EnergyLevel = 100;
    }

    protected virtual void DecreaseEnergy(float amount)
    {
        EnergyLevel -= amount;
        if (EnergyLevel <= 0)
        {
            Debug.Log(gameObject.name + " died...");
            Destroy(gameObject);
        }
    }
}
