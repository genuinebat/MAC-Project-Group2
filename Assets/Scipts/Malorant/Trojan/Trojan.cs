using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    //IDamagable and IScannable are interfaces to make sure that there is always a GetHit and Scan function
    public class Trojan : MonoBehaviour, IDamageable, IScannable
    {
        //to chekck if the gameobject has been scanned
        public bool Scanned { get; private set; }

        void Start()
        {
            Scanned = false;
        }

        //if the Trojan is shot
        public void GetHit()
        {
            //but is not scanned then return
            if (!Scanned) return;
            //if it is scanned then it will destroy itself
            Destroy(gameObject);
        }

        // if the trojan is scanned
        public void Scan()
        {
            //if it has been scanned 
            Scanned = true;
            //change the tag of the enemy to be Enemy
            gameObject.tag = "Enemy";
        }
    }
}