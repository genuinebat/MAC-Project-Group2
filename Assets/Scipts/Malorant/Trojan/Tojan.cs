using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class Trojan : MonoBehaviour, IDamageable, IScannable
    {
        public bool Scanned { get; private set; }

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