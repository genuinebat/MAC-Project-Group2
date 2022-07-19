using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class BotEgg : MonoBehaviour, IDamageable
    {
        public GameObject enemyPrefab;
        public Renderer rend;

        public float dissapearSpeed;

        GameObject enemyStore;

        void Start()
        {
            StartCoroutine(FadeOutMaterial(dissapearSpeed));
            enemyStore = GameObject.Find("Spawner");
        }

        public void GetHit()
        {
            Destroy(gameObject);
        }

        IEnumerator FadeOutMaterial(float fadeSpeed)
        {
            yield return new WaitForSeconds(1f);

            Color matColor = rend.material.color;
            float alphaValue = rend.material.color.a;

            while (rend.material.color.a > 0f)
            {
                alphaValue -= Time.deltaTime / fadeSpeed;
                rend.material.color = new Color(matColor.r, matColor.g, matColor.b, alphaValue);
                yield return null;
            }

            GameObject newEnemy = Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.Euler(0f, 90f, 0f));
            newEnemy.transform.parent = enemyStore.transform;

            Destroy(gameObject);
        }
    }
}
