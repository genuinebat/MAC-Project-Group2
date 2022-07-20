using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class ShootBtn : MonoBehaviour
    {
        [Header("Reference Variables")]
        public RectTransform Middle;
        public RectTransform Inner;
        public GameObject ShootBackground;
        
        PressingButton script;

        void Start()
        {
            script = GetComponent<PressingButton>();
        }

        void Update()
        {
            Middle.eulerAngles -= new Vector3(0f, 0f, 0.4f);

            Inner.eulerAngles += new Vector3(0f, 0f, 0.3f);

            if (script.Pressing) ShootBackground.SetActive(true);
            else ShootBackground.SetActive(false);
        }
    }
}