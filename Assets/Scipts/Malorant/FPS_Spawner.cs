using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia.ARFoundation;

namespace Malorant
{
    public class FPS_Spawner : MonoBehaviour
    {
        public Transform ImageTarget;

        //list to store enemy GamObjects
        public List<GameObject> enemies = new List<GameObject>();
        //set the delay in between the enemies running
        public int timeBetweenEnemies;

        //boolean to check if the timer is still running
        bool delayRunning = true;

        void Update()
        {
            if (!delayRunning && enemies.Count > 0)
            {
                //gets a random enemy from the list
                int enemyType = Random.Range(0, enemies.Count);

                //timer to ensure that the code doesnt run too fast and spawn all enemies at once
                StartCoroutine(Timer(timeBetweenEnemies));

                //instantiate enemy
                var newEnemy = Instantiate(enemies[enemyType], transform.position, Quaternion.identity);

                newEnemy.GetComponent<FPSEnemy>().ImageTarget = ImageTarget;

                //child enemy to the spawnner
                newEnemy.transform.parent = gameObject.transform;

                //remove the enemy from the list so that it only spawns once
                enemies.RemoveAt(enemyType);
            }

            //if (imageTargetTemplate.CurrentStatus == TrackableBehaviour.Status.DETECTED) ;
        }


        //integer so that the delay can be controlled in the inspector
        IEnumerator Timer(int timeBetween)
        {
            //set the boolean to true to see if the timer is running
            delayRunning = true;

            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(timeBetween);

            //set the boolean to false when the timer stops runnning
            delayRunning = false;
        }


        public void EnterView()
        {
            delayRunning = false;
        }
    }
}