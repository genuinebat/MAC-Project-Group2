using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pathfinding;

namespace RansomMan
{
    public class ByteTracker : MonoBehaviour
    {
        public TMP_Text TrackerTxt;
        public GameObject WinUI;
        
        [HideInInspector]
        public int Collected;
        [HideInInspector]
        public List<Node> Temp = new List<Node>();
        [HideInInspector]
        public bool GameStarted;

        int total;
        bool won;

        void Start()
        {
            won = false;
            Collected = 0;
            GameStarted = false;
        }

        void Update()
        {
            TrackerTxt.text = "Bytes Collected: " + (Collected + Temp.Count).ToString() +  " / " + total;

            if (GameStarted && Collected >= total && !won)
            {
                WinGame();
            }
        }

        public void SetTotal()
        {
            total = GameObject.FindGameObjectsWithTag("Byte").Length;
        }

        void WinGame()
        {
            won = true;
            Time.timeScale = 0f;
            WinUI.SetActive(true);
        }
    }
}
