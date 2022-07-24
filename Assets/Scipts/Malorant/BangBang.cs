using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class BangBang : MonoBehaviour
    {
        [Header("Prefabs")]
        public GameObject hitEffect;

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
                Instantiate(hitEffect,hit.point, Quaternion.identity);

                IDamageable i = hit.transform.gameObject.GetComponent<IDamageable>();

                if (i != null) i.GetHit();

                timer.CheckWin();
            }
        }
    }
}
