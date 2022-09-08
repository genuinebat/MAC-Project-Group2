using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class Botware : MonoBehaviour, IDamageable
    {
        //botegg prefab
        public GameObject eggPrefab;
        //to set how long is the delay between eggs being spawnned
        public float timebetweenSpawn;
        //speed of movement for the botware
        public float Speed;

        //gameobject to child what is being spawnned
        GameObject enemyStore;
        //transform (world position) of where the image target is
        Transform imageTarget;
        //where the bot is heading to 
        Vector3 targetLocation;
        //constraints on how far away the botware can move
        float minX, maxX, minY, maxY, minZ, maxZ;

        void Start()
        {
            //assigns the spawnner gameobejct to the enemystore variable
            enemyStore = GameObject.Find("Spawner");
            imageTarget = GameObject.Find("ImageTarget").transform;

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

        //if the enemy is shot
        public void GetHit()
        {
            //destroy enemy
            Destroy(gameObject);
        }

        //integer so that the delay can be controlled in the inspector
        IEnumerator SpawnEgg(float timeBetween)
        {
            //yield on a new YieldInstruction that waits for a number of seconds which can be set in the inspector.
            yield return new WaitForSeconds(timeBetween);
            //instantiates an egg gameobject whic h will later spawn more eggs
            GameObject newEnemy = Instantiate(eggPrefab, gameObject.transform.position, Quaternion.identity);
            //childs the egg to the enemy store so that it can be checked in the inspector
            newEnemy.transform.parent = enemyStore.transform;
            //repeats the function constantly unless the botware is destroyed 
            StartCoroutine(SpawnEgg(timeBetween));
        }

        void SetBoundaries()
        {
            minX = imageTarget.position.x - 5;
            maxX = imageTarget.position.x + 5;
            minY = imageTarget.position.y - 5;
            maxY = imageTarget.position.y + 5;
            minZ = imageTarget.position.z - 3;
            maxZ = imageTarget.position.z;
        }

        IEnumerator PeriodicallySetBoundaries()
        {
            for (; ; )
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
