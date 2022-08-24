using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject Obj;
    
    float elap;
    bool open, wait;

    void Start()
    {
        PlayerPrefs.SetString("NextStage", "Completed");

        open = false;
        wait = false;
        Obj.SetActive(false);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, Time.deltaTime);

        if (Input.GetMouseButtonDown(0) && !wait)
        {
            if (open) StartCoroutine(Close());
            else
            {
                open = true;
                Obj.SetActive(true);
            }
        }
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(.2f);
        
        Obj.SetActive(false);
        open = false;

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        wait = true;
        yield return new WaitForSeconds(.3f);
        wait = false;
    }
}