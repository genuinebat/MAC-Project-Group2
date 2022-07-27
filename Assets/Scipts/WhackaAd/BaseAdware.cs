using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAdware : MonoBehaviour
{
    public GameObject XButton;

    [HideInInspector]
    public bool IsDestroyable { get; set; }

    //GameObject currentHitObject;

    void CloseAd(GameObject tappedOn)
    {
        if (IsDestroyable == true)
        {
            Destroy(tappedOn);
        }
    }

    void fixedUpdate()
    {
        //public GameObject particle;
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                // Create a particle if hit
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.position == XButton.transform.position)
                    {
                        CloseAd(hit.transform.gameObject);
                    }
                }
            }
        }
    }
}
