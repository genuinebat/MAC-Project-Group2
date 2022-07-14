using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotEgg : MonoBehaviour
{
    public float dissapearSpeed;
    public Renderer rend;
    public GameObject enemyPrefab;
    private GameObject enemyStore;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOutMaterial(dissapearSpeed));
        enemyStore = GameObject.Find("Spawner");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeOutMaterial(float fadeSpeed)
    {
        yield return new WaitForSeconds(1f);

        //Renderer rend = gameObject.transform.GetComponent<Renderer>();
        Color matColor = rend.material.color;
        float alphaValue = rend.material.color.a;

        while (rend.material.color.a > 0f)
        {
            alphaValue -= Time.deltaTime / fadeSpeed;
            rend.material.color = new Color(matColor.r, matColor.g, matColor.b, alphaValue);
            yield return null;
        }

        GameObject newEnemy = Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
        newEnemy.transform.parent = enemyStore.transform;

        Destroy(gameObject);
        //rend.material.color = new Color(matColor.r, matColor.g, matColor.b, 0f);
    }
}
