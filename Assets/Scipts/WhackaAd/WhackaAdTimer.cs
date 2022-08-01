using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WhackaAd
{
    public class WhackaAdTimer : MonoBehaviour
    {
        [Header("Referencce variables")]
        public TMP_Text TimerUI;
        public TMP_Text EnemyCounter;
        public GameObject WinUI;
        public GameObject LoseUI;
        public Image EnemyCounterFill;

        [Header("Functionality Variables")]
        public float TimeMin;
        public float TimeSec;
        public int MaxAds;

        [HideInInspector]
        public float TimeLeft;

        public bool GameStarted { private get; set; }

        Transform spawner;

        bool won;

        // Start is called before the first frame update
        void Start()
        {
            spawner = GameObject.Find("Spawner").transform;

            TimeLeft = TimeMin * 60 + TimeSec;
            won = false;

            GameStarted = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!GameStarted) return;

            // modifying the UI for showing the number of enemies
            EnemyCounter.SetText("Number of Adware: " + spawner.childCount + " /  " + MaxAds);

            EnemyCounterFill.fillAmount = Mathf.Lerp(EnemyCounterFill.fillAmount, (float) spawner.childCount / MaxAds, Time.deltaTime * 10);

            // updating and changing the timer UI
            TimeLeft -= Time.deltaTime;

            string minutes = ((int)TimeLeft / 60).ToString("00");
            string seconds = Mathf.Round(TimeLeft % 60).ToString("00");

            TimerUI.text = minutes + ":" + seconds;

            if (TimeLeft < 0)
            {
                TimeLeft = 0;
                Time.timeScale = 0;
                if (won == true)
                {
                    WinUI.SetActive(true);
                }
            }

            if (spawner.childCount >= MaxAds)
            {
                won = false;
                GameOver();
            }
        }

        // function to and the game
        public void GameOver()
        {
            Time.timeScale = 0;
            LoseUI.SetActive(true);
        }
    }
}
