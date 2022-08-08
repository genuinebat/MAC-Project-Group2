using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RansomMan
{
    public class ByteTracker : MonoBehaviour
    {
        public int StartByte;
        public int CurrentByte;
        public List<GameObject> TempByte;
        public Transform NodeStore;
        public TMP_Text tracker;

        // Start is called before the first frame update
        void Start()
        {
            // int children = NodeStore.childCount;
            // for (int i = 0; i < children; ++i)
            //     if (NodeStore.GetChild(i).gameObject.tag == ("Byte"))
            //     {
            //         StartByte++;
            //     }
            StartByte = GameObject.FindGameObjectsWithTag("Byte").Length;
            tracker.text = ((StartByte - CurrentByte), "/", StartByte).ToString();

        }

        // Update is called once per frame
        void Update()
        {
            CurrentByte = GameObject.FindGameObjectsWithTag("Byte").Length;
            tracker.text = ((StartByte - CurrentByte), "/", StartByte).ToString();

        }

    }
}
