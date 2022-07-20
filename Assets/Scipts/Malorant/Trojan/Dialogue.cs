using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    //to store the player camera location
    GameObject playerCam;
    // Start is called before the first frame update
    void Start()
    {
        //assign the player camera to the playeyr Cam
        playerCam = GameObject.Find("ARCamera");
    }

    // Update is called once per frame
    void Update()
    {
        //makes the horse face the player at all times
        transform.LookAt(new Vector3(0, playerCam.transform.position.y, playerCam.transform.position.z));

    }
}
