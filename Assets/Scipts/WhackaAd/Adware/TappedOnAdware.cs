using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackaAd
{
    public class TappedOnAdware : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.name == "CloseAD")
                    {
                        AdwareEffects effects =
                            hit
                                .transform
                                .gameObject
                                .GetComponentInParent<AdwareEffects>();

                        if (effects.Teleport)
                        {
                            //GameObject.Instantiate(TeleEffect, CurrentLocation.position, Quaternion.identity);

                            effects.CheckTeleport(hit.transform);
                        }
                        else
                        {
                            hit
                                .transform
                                .gameObject
                                .GetComponentInParent<BaseAdware>()
                                .CloseAd();
                        }
                    }
                    else if (hit.transform.gameObject.tag == "ADWare")
                    {
                        hit
                            .transform
                            .gameObject
                            .GetComponentInParent<BaseAdware>()
                            .DuplicateEnemy();
                    }
                }
            }
        }
    }
}
