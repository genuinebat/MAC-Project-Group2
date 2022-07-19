using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class AdwareX : MonoBehaviour, IDamageable
    {
        public void GetHit()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}