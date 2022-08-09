using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WhackaAd
{
    public class WhackaAdTime : MonoBehaviour
    {
        [Header("Reference variables")]
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

        TappedOnAdware tapScript;

        Transform spawner;

        bool won;

        // Start is called before the first frame update
        void Start()
        {
            spawner = GameObject.Find("Spawner").transform;

            tapScript = GetComponent<TappedOnAdware>();

            TimeLeft = TimeMin * 60 + TimeSec;

            GameStarted = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!GameStarted) return;

            // modifying the UI for showing the number of enemies
            EnemyCounter
                .SetText("Number of Adware: " +
                spawner.childCount +
                " /  " +
                MaxAds);

            EnemyCounterFill.fillAmount =
                Mathf
                    .Lerp(EnemyCounterFill.fillAmount,
                    (float) spawner.childCount / MaxAds,
                    Time.deltaTime * 10);

            // updating and changing the timer UI
            TimeLeft -= Time.deltaTime;

            string minutes = ((int) TimeLeft / 60).ToString("00");
            string seconds = Mathf.Round(TimeLeft % 60).ToString("00");

            TimerUI.text = minutes + ":" + seconds;

            if (TimeLeft < 0)
            {
                TimeLeft = 0;
                Time.timeScale = 0;
                WinUI.SetActive(true);
            }

            if (spawner.childCount >= MaxAds)
            {
                GameOver();
            }
        }

        // function to and the game
        public void GameOver()
        {
            tapScript.GameEnd = true;
            GameStarted = false;
            Time.timeScale = 0;
            LoseUI.SetActive(true);
        }
    }
}
