using System.Collections;
using UnityEngine;

public partial class Animal
{
    [Range(0, 1f)]
    public float Fertility = 0.1f;
    public float MatingCooldown = 10;

    public bool Fertile { get; private set; } = true;

    protected virtual void AttemptMatingWithTarget(GameObject gameObject)
    {
        if (!Fertile) return;

        var otherAnimal = gameObject.GetComponent<Animal>();
        if (otherAnimal == null) return;

        if (otherAnimal.Fertile && EnergyLevel > 30 && Random.Range(0, 1f) <= Fertility)
        {
            StartCoroutine(SpawnBabyAfterDelay(3f));
        }
    }

    protected IEnumerator SpawnBabyAfterDelay(float delay)
    {
        Debug.Log("Spawning new baby");
        StartCoroutine(FertilityCooldown(MatingCooldown));
        yield return new WaitForSeconds(delay);
        SpawnBaby();
    }

    protected virtual void SpawnBaby()
    {
        DecreaseEnergy(15);
        GameObject.Instantiate(gameObject, transform.position, Quaternion.identity);
    }

    protected IEnumerator FertilityCooldown(float time)
    {
        Fertile = false;
        yield return new WaitForSeconds(time);
    }
}