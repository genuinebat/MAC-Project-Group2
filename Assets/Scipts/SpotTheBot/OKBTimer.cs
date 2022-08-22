using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace OKB
{
    public class OKBTimer : MonoBehaviour
    {
        public GameObject LoseUI;
        public TMP_Text TimerUI;
        public float TimeMin;
        public float TimeSec;
        public float timeLeft;

        public OKBSM OKBSMScript;

        public bool GameStart;

        // Start is called before the first frame update
        void Start()
        {
            //set timescale to zero and only set it back to 1 in initialize (OKBSM)
            timeLeft = TimeMin * 60 + TimeSec;
            GameStart = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!GameStart) return;

            timeLeft -= Time.deltaTime;
            string minutes = ((int)timeLeft / 60).ToString("00");
            string seconds = Mathf.Round(timeLeft % 60).ToString("00");

            TimerUI.text = minutes + ":" + seconds;
            if (timeLeft < 0)
            {
                Time.timeScale = 0;
                timeLeft = 0;
                LoseUI.SetActive(true);
                OKBSMScript.tryCount++;
            }

        }
    }
}
