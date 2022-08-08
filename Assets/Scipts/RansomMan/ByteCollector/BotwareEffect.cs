using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RansomMan
{
    public class BotwareEffect : MonoBehaviour
    {
        public ByteTracker ByteTrackerScript;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                int i = 0;
                //ByteTrackerScript.TempByte.Add(other.gameObject);
                while (i < ByteTrackerScript.TempByte.Count)
                {
                    ByteTrackerScript.TempByte[i].gameObject.SetActive(true);
                }
            }

        }
    }
}
