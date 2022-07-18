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
    public Text timerUI;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeMin * 60 + timeSec;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        string minutes = ((int)timeLeft / 60).ToString("00");
        string seconds = Mathf.Round(timeLeft % 60).ToString("00");
        timerUI.text = minutes + ":" + seconds;

        if (timeLeft < 0)
        {
            timeLeft = 0;
        }
    }
}
