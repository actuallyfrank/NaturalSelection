using System.Collections;
using UnityEngine;

public partial class Animal
{
    protected virtual void AttemptMatingWithTarget(GameObject gameObject)
    {
        var otherAnimal = gameObject.GetComponent(this.GetType());
        if (otherAnimal == null)
        {
            Debug.Log("Trying to mate with the wrong type");
            return;
        }

        if (Random.Range(0, 1f) <= Fertility)
        {
            StartCoroutine(SpawnNewBaby(1f));
        }
    }

    protected IEnumerator SpawnNewBaby(float delay)
    {
        Debug.Log("Spawning new baby");
        yield return new WaitForSeconds(delay);
        GameObject.Instantiate(gameObject, transform.position, Quaternion.identity);
    }
}