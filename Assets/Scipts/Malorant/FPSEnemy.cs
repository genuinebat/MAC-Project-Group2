using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class FPSEnemy : MonoBehaviour
{
    public int health;
    public string type;

    // Start is called before the first frame update
    void Start()
    {
        health = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        transform.LookAt(Camera.main.transform.position);
        //transform.position(Camera.main.transform.position);
    }


}
