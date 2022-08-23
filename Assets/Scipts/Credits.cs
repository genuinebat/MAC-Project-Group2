using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject Obj;

    void Start()
    {
        PlayerPrefs.SetString("NextStage", "Completed");
        StartCoroutine(Delay());
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, Time.deltaTime);
    }

    IEnumerator Delay()
    {
        Obj.SetActive(false);
        yield return new WaitForSeconds(20f);
        Obj.SetActive(true);
    }
}