using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class BangBang : MonoBehaviour
    {
        public GameObject hitEffect;
        private bool delayRunning;

        public void Bang()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject shotvfx = Instantiate(hitEffect,hit.point, Quaternion.identity);
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.gameObject.GetComponent<IDamageable>().GetHit();

                }
            }
        }

        //IEnumerator DelayShot(RaycastHit hit)
        //{
        //    //yield return new WaitForSeconds(0.5f);
        //    if (hit.transform.tag == "Enemy")
        //    {
        //        hit.transform.gameObject.GetComponent<IDamageable   >().GetHit();

        //    }
        //    yield return null;
        //}
    }
}
