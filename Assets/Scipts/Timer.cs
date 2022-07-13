using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeMin;
    public float timeSec;
    private float timeLeft;
    public Text timerUI; // used for showing countdown from 3, 2, 1 

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeMin * 60 + timeSec;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        string minutes = ((int)timeLeft / 60).ToString();
        string seconds = Mathf.Round(timeLeft % 60).ToString();
        timerUI.text = minutes + ":" + seconds;

        if (timeLeft < 0)
        {
            Debug.Log("timer done");
            //Do something useful or Load a new game scene depending on your use-case
        }
    }
}
