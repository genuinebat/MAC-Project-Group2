using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class BangBang : MonoBehaviour
    {
        public GameObject hitEffect;
        public MalorantGameState timer;

        public void Bang()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(hitEffect,hit.point, Quaternion.identity);

                if (hit.transform.tag == "Enemy")
                {
                    timer.CheckWin();
                    hit.transform.gameObject.GetComponent<IDamageable>().GetHit();
                }
            }
        }
    }
}
