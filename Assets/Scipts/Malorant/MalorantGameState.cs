using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Malorant
{
    public class MalorantGameState : MonoBehaviour
    {
        [Header("Reference Variables")]
        public GameObject LoseUI;
        public GameObject WinUI;
        public Text TimerUI;

        [Header("Functionality Variables")]
        public float TimeMin;
        public float TimeSec;

        [HideInInspector]
        public GameObject Enemies;

        [HideInInspector]
        public bool GameStarted;

        [HideInInspector]
        public float timeLeft;

        void Start()
        {
            Enemies = GameObject.Find("Spawner");

            timeLeft = TimeMin * 60 + TimeSec;
            GameStarted = false;
        }

        void Update()
        {
            // checking if the game has started already
            if (GameStarted == true)
            {
                timeLeft -= Time.deltaTime;

                string minutes = ((int)timeLeft / 60).ToString("00");
                string seconds = Mathf.Round(timeLeft % 60).ToString("00");

                TimerUI.text = minutes + ":" + seconds;

                if (timeLeft < 0)
                {
                    timeLeft = 0;
                    Time.timeScale = 0;

                    LoseUI.SetActive(true);
                }
            }
        }

        // function to check if all of the malwares have been destroyed
        public void CheckWin()
        {
            if (Enemies.transform.childCount <= 1)
            {
                timeLeft = 0;
                Time.timeScale = 0;

                WinUI.SetActive(true);
            }
        }
    }

}