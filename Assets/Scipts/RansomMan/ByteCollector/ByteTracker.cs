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
        
        [HideInInspector]
        public int Collected = 0;
        [HideInInspector]
        public List<Node> Temp = new List<Node>();

        int total;

        void Update()
        {
            TrackerTxt.text = "Bytes Collected: " + (Collected + Temp.Count).ToString() +  " / " + total;
        }

        public void SetTotal()
        {
            total = GameObject.FindGameObjectsWithTag("Byte").Length;
        }
    }
}
