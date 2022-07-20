using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    GameObject playerCam;
    // Start is called before the first frame update
    void Start()
    {
        playerCam = GameObject.Find("ARCamera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(0, playerCam.transform.position.y, playerCam.transform.position.z));

    }
}
