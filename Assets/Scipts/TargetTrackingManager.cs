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

    public void OpenWhenSeen(GameObject UI)
    {
        UI.SetActive(true);
    }

    public void closeWhenUnseen(GameObject UI)
    {
        UI.SetActive(false);
    }
}
