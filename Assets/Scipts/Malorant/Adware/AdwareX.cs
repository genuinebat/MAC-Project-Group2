using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class AdwareX : MonoBehaviour, IDamageable
    {
        //if the player shoots the collider on the weakspot destroy the adware
        public void GetHit()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}