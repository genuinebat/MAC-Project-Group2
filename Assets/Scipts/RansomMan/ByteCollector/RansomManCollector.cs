using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RansomMan
{
    public class RansomManCollector : MonoBehaviour
    {
        public ByteTracker ByteTrackerScript;

        void Start()
        {
            ByteTrackerScript = GameObject.Find("RansomMan").GetComponent<ByteTracker>();

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Byte")
            {
                ByteTrackerScript.CurrentByte -= 1;
                ByteTrackerScript.TempByte.Add(other.gameObject);
                other.gameObject.SetActive(false);
            }
            else if (other.gameObject.tag == "Backup")
            {
                int i = 0;
                while (i < ByteTrackerScript.TempByte.Count)
                {
                    Destroy(ByteTrackerScript.TempByte[i].gameObject);
                }
            }
        }
    }
}
