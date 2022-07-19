using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class Botware : MonoBehaviour, IDamageable
    {
        public GameObject eggPrefab;
        
        public float timebetweenSpawn;
        public float Speed;

        GameObject eggStore;
        Transform imageTarget;
        Vector3 targetLocation;
        float minX, maxX, minY, maxY, minZ, maxZ;

        void Start()
        {
            eggStore = GameObject.Find("Spawner");
            imageTarget = GameObject.Find("Target").transform;

            StartCoroutine(SpawnEgg(timebetweenSpawn));
            StartCoroutine(PeriodicallySetBoundaries());

            targetLocation = transform.position;
        }

        void Update()
        {
            if (Vector3.Distance(transform.position, targetLocation) < 0.01f)
            {
                SetNewRandomTargetLocation();
            }
            else
            {
                MoveToTargetLocation();
            }
        }

        public void GetHit()
        {
            Destroy(gameObject);
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

        void SetBoundaries()
        {
            minX = imageTarget.position.x - 5;
            maxX = imageTarget.position.x + 5;
            minY = imageTarget.position.y -  5;
            maxY = imageTarget.position.y + 5;
            minZ = imageTarget.position.z - 3;
            maxZ = imageTarget.position.z;
        }

        IEnumerator PeriodicallySetBoundaries()
        {
            for (;;)
            {
                SetBoundaries();
                yield return new WaitForSeconds(.5f);
            }
        }

        void MoveToTargetLocation()
        {
            transform.position = 
                Vector3.MoveTowards(
                    transform.position,
                    targetLocation, 
                    Speed * Time.deltaTime
                );
        }

        void SetNewRandomTargetLocation()
        {
            targetLocation =
                new Vector3(
                    Random.Range(minX, maxX),
                    Random.Range(minY, maxY),
                    Random.Range(minZ, maxZ)
                );
        }
    }
}
