using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float Nutrients = 10;

    private Collider collider;
    private MeshRenderer meshRenderer;
    protected void OnEnable()
    {
        collider = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public float Eat()
    {
        collider.enabled = false;
        meshRenderer.enabled = false;
        StartCoroutine(RespawnFoodAfterDelay(10));

        return Nutrients;
    }

    protected IEnumerator RespawnFoodAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        collider.enabled = true;
        meshRenderer.enabled = true;
    }
}
