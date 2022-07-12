using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TargetTrackingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableObject(GameObject go)
    {
        go.SetActive(true);
    }

    public void DisableObject(GameObject go)
    {
        go.SetActive(false);
    }
}
