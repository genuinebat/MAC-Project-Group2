using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WhackaAd
{
    public class WhackaAdTImer : MonoBehaviour
    {
        [Header("Referencce variables")]
        public TMP_Text TimerUI;

        [Header("Functionality Variables")]
        public float TimeMin;
        public float TimeSec;

        [HideInInspector]
        public float TimeLeft;

        bool won;

        // Start is called before the first frame update
        void Start()
        {
            TimeLeft = TimeMin * 60 + TimeSec;
            won = false;

        }

        // Update is called once per frame
        void Update()
        {
            if (!won) TimeLeft -= Time.deltaTime;

            string minutes = ((int)TimeLeft / 60).ToString("00");
            string seconds = Mathf.Round(TimeLeft % 60).ToString("00");

            TimerUI.text = minutes + ":" + seconds;

        }

        public void CheckWin()
        {

        }

    }

}
