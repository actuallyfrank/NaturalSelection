using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public partial class Animal : MonoBehaviour, IAnimal
{
    public float EnergyLevel = 100;
    [Range(0, 1f)]
    public float Fertility = 0.1f;
    [Range(0.3f, 2f)]
    public float MovementSpeed = 1;

    private NavMeshAgent navMeshAgent;
    private FieldOfView fieldOfView;

    protected virtual void OnEnable()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        fieldOfView = GetComponent<FieldOfView>();

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
        if (fieldOfView.visibleTargets.Count > 0)
        {
            Transform target = fieldOfView.visibleTargets[0].transform;
            SetNewTargetLocation(Vector3.MoveTowards(transform.position, target.position, MovementSpeed));
            AttemptMatingWithTarget(target.gameObject);
        }
        else
        {
            SetNewTargetLocation(transform.position + Random.insideUnitSphere * MovementSpeed);
        }        
    }

    protected virtual void SetNewTargetLocation(Vector3 targetLocation)
    {
        if (navMeshAgent != null)
            navMeshAgent.SetDestination(targetLocation);
    }

    protected virtual void IncreaseEnergy(float amount)
    {
        EnergyLevel += amount;
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
