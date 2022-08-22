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
            int subTotal = Collected + Temp.Count;

            TrackerTxt.text = "Bytes Collected: " + subTotal +  " / " + total;

            if (GameStarted && subTotal >= total)
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
            GameStarted = false;
            WinUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
