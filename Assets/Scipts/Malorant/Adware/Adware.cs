using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class Adware : MonoBehaviour, IDamageable, IScannable
    {
        class Quad
        {
            public float MinX, MaxX, MinY, MaxY, Z;
            public Quad()
            {

            }
        }
        public bool Scanned { get; private set; }

        int quad;

        void Start()
        {
            quad = 0;
        }

        void Update()
        {
            // swith(quad)
            // {
            //     case 1:

            //         break;
                
            //     case 2:

            //         break;

            //     case 3:

            //         break;

            //     case 4:

            //         break;
            // }
        }

        public void GetHit()
        {
            if (!Scanned) return;
        }

        public void Scan()
        {
            Scanned = true;

            gameObject.tag = "Enemy";
        }
    }
}