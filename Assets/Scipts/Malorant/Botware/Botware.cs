using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botware : MonoBehaviour
{
    public GameObject eggPrefab;
    public float timebetweenSpawn;
    private GameObject eggStore;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEgg(timebetweenSpawn));
        eggStore = GameObject.Find("EggStore");
    }

    //integer so that the delay can be controlled in the inspector
    IEnumerator SpawnEgg(float timeBetween)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(timeBetween);

        GameObject newEnemy = Instantiate(eggPrefab, gameObject.transform.position, Quaternion.identity);
        newEnemy.transform.parent = eggStore.transform;

        StartCoroutine(SpawnEgg(timeBetween));
    }
}
