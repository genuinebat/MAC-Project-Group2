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
                //Clic i = hit.transform.gameObject.GetComponent<Clicked>();
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.name == "CloseAD")
                    {
                        Debug.Log("x");
                            hit.transform.gameObject
                                .GetComponentInParent<BaseAdware>()
                                .CloseAd();
                    }
                    else if (hit.transform.gameObject.tag == "ADWare")
                    {
                        Debug.Log("click on panel");
                        hit.transform.gameObject
                            .GetComponentInParent<BaseAdware>()
                            .DuplicateEnemy();
                    }
                }
            }
        }
    }
}
