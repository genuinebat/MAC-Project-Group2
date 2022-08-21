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

        void Start()
        {
            Collected = 0;
            GameStarted = false;
        }

        void Update()
        {
            TrackerTxt.text = "Bytes Collected: " + (Collected + Temp.Count).ToString() +  " / " + total;

            Debug.Log(Collected);
            Debug.Log(total);
            Debug.Log("GameStarted");
            Debug.Log(GameStarted);
            if (GameStarted && Collected >= total)
            {
                Debug.Log("Inside");
                WinGame();
            }
        }

        public void SetTotal()
        {
            total = GameObject.FindGameObjectsWithTag("Byte").Length;
        }

        void WinGame()
        {
            Debug.Log("running funciton");
            GameStarted = false;
            WinUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
