using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class BangBang : MonoBehaviour
    {
        [Header("Prefabs")]
        public GameObject HitEffect;

        [Header("Reference Variables")]
        public MalorantGameState timer;

        // function that is used to instantiate the particle effect and
        // trigger the GetHit() function of all enemies with IDamageable
        public void Bang()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // spawning the particle effect
                Instantiate(HitEffect, hit.point, Quaternion.identity);

                IDamageable d = hit.transform.gameObject.GetComponent<IDamageable>();

                if (d != null && hit.transform.gameObject.tag == "Enemy")
                {
                    d.GetHit();
                    timer.CheckWin();
                }
                
                IScannable s = hit.transform.gameObject.GetComponent<IScannable>();
                
                if (s !=null && hit.transform.gameObject.tag == "Scannable")
                {
                    s.GetHit();
                }
            }
        }
    }
}
