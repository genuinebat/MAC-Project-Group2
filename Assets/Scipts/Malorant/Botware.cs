using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botware : MonoBehaviour
{
    //boolean to check if the timer is still running
    bool delayRunning = true;

    // Update is called once per frame
    void Update()
    {
        
    }

    //integer so that the delay can be controlled in the inspector
    IEnumerator Timer(int timeBetween)
    {
        //set the boolean to true to see if the timer is running
        delayRunning = true;

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(timeBetween);

        //set the boolean to false when the timer stops runnning
        delayRunning = false;
    }
}
