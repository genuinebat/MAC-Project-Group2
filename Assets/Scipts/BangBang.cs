using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangBang : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Bang()
    {
        //GetComponent<Collider>().Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)));
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Destroy(hit.transform.gameObject);

        }
    }
}
