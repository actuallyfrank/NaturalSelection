using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bunny : MonoBehaviour
{
    [Range(0.3f, 1.5f)]
    public float jumpRange = 1f;
    [Range(0, 100)]
    public float energyLevel = 100;
    public float visionRadius = 1;
    public float fertility = 0.1f;


    NavMeshAgent navMeshAgent;
    FieldOfView fieldOfView;
    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        fieldOfView = GetComponent<FieldOfView>();
        fieldOfView.viewRadius = visionRadius;
    }

    private void OnEnable()
    {
        StartCoroutine(UpdateBehaviour(1));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        
    }

    IEnumerator UpdateBehaviour(float jumpLoopTime)
    {
        while(true)
        {
            yield return new WaitForSeconds(jumpLoopTime);

            List<Transform> targets = fieldOfView.visibleTargets;
            
            if (targets.Count > 0)
            {
                Transform randomTarget = targets[Random.Range(0, targets.Count)];
                if (randomTarget.GetComponent<Bunny>() && Random.Range(0, 1f) < fertility)
                {
                    Debug.Log("Mating");
                    StartCoroutine(SpawnBunnyWithDelay(transform.position, 1f));
                    DecreaseEnergy(20);
                }
                {
                    Jump(randomTarget.position);
                }

                
            }
            else
            {
                Vector2 randomCircle = Random.insideUnitCircle;
                Vector3 randomOffset = new Vector3(randomCircle.x, 0, randomCircle.y) * jumpRange;
                Jump(transform.position + randomOffset);
            }           
        }
    }

    private IEnumerator SpawnBunnyWithDelay(Vector3 targetLocation, float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject.Instantiate(gameObject, targetLocation, Quaternion.identity);
    }

    private void DecreaseEnergy(float amount)
    {
        energyLevel -= amount;
        if (energyLevel < 0)
        {
            Debug.Log(gameObject.name + " died");
            Destroy(gameObject);
        }
    }

    private void Jump(Vector3 targetLocation)
    {        
        DecreaseEnergy(Vector3.Distance(transform.position * 2, targetLocation));

        if (navMeshAgent) 
            navMeshAgent.SetDestination(targetLocation);
    }
}
