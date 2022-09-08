using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class BotEgg : MonoBehaviour, IDamageable
    {
        //botware prefab
        public GameObject EnemyPrefab;

        //renderer of the egg (to change the alpha values
        public Renderer rend;

        //gameobject to child the spawned eggs to (for checking the win conditions)
        GameObject enemyStore;

        void Start()
        {
            //start the coroutine to make the egg fade away
            StartCoroutine(FadeInMaterial());

            //assign the enemystore Gameobject
            enemyStore = GameObject.Find("Spawner");
        }

        //if the egg is shot destroy itself
        public void GetHit()
        {
            Destroy(gameObject);
        }

        IEnumerator FadeInMaterial()
        {
            ////wait for one second
            yield return new WaitForSeconds(1.5f);

            //get the current color of the material
            Color matColor = rend.material.color;

            //get the starting alpha value
            float blueValue = rend.material.color.b;

            //while the alpha value of the egg is more than zero
            while (rend.material.color.b < 1)
            {
                //slowly decrease the colour of the egg
                blueValue += Time.deltaTime / 1f;
                rend.material.color =
                    new Color(matColor.r, matColor.g, blueValue);
                yield return null;
            }

            if (rend.material.color.b >= 0.9)
            {
                //when the egg is completely see through
                GameObject newEnemy =
                    Instantiate(EnemyPrefab,
                    gameObject.transform.position,
                    Quaternion.Euler(0f, 90f, 0f));

                //child the enemy to the enmystore gameobject to check win condition
                newEnemy.transform.parent = enemyStore.transform;

                //destroys the egg
                Destroy(gameObject);
            }
        }
    }
}
