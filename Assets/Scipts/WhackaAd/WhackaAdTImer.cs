using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WhackaAd
{
    public class WhackaAdTImer : MonoBehaviour
    {
        [Header("Referencce variables")]
        public TMP_Text TimerUI;
        public TMP_Text EnemyCounter;
        public GameObject WinUI;
        public GameObject LoseUI;
        public Slider EnemyCounterSlider;

        [Header("Functionality Variables")]
        public float TimeMin;
        public float TimeSec;
        public int MaxAds;

        [HideInInspector]
        public float TimeLeft;
        int ADTempCount;

        bool won;

        // Start is called before the first frame update
        void Start()
        {
            TimeLeft = TimeMin * 60 + TimeSec;
            won = false;

            EnemyCounterSlider.maxValue = MaxAds;
        }

        // Update is called once per frame
        void Update()
        {
            //Display enemies
            ADTempCount = GameObject.Find("Spawner").transform.childCount;
            EnemyCounter.SetText(ADTempCount + "/ " + MaxAds);
            EnemyCounterSlider.value = ADTempCount;

            //Timer
            TimeLeft -= Time.deltaTime;
            string minutes = ((int)TimeLeft / 60).ToString("00");
            string seconds = Mathf.Round(TimeLeft % 60).ToString("00");

            TimerUI.text = minutes + ":" + seconds;
            if (TimeLeft < 0)
            {
                TimeLeft = 0;
                Time.timeScale = 0;
                if (won = true)
                {
                    WinUI.SetActive(true);
                }
            }

            if (GameObject.Find("Spawner").transform.childCount >= MaxAds)
            {
                won = false;
                GameOver();
            }
        }

        public void GameOver()
        {
            Time.timeScale = 0;
            LoseUI.SetActive(true);
        }
    }
}
