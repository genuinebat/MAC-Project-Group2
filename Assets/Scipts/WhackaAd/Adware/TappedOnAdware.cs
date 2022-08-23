using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackaAd
{
    public class TappedOnAdware : MonoBehaviour
    {
        public GameObject CloseParticles;
        public GameObject SpawnParticles;
        [HideInInspector]
        public bool GameEnd;
        [HideInInspector]
        public bool Paused;

        void Start()
        {
            GameEnd = false;
            Paused = false;
        }

        void Update()
        {
            if (GameEnd || Paused) return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.name == "CloseAD")
                    {
                        AdwareEffects effects =
                            hit.transform.gameObject.GetComponentInParent<AdwareEffects>();

                        if (effects.Teleport)
                        {
                            effects.CheckTeleport(hit.transform);
                        }
                        else
                        {
                            //Instantiate(CloseParticles, hit.transform.parent.GetChild(0).position, Quaternion.identity); 
                            hit.transform.gameObject.GetComponentInParent<BaseAdware>().CloseAd();
                        }
                    }
                    else if (hit.transform.gameObject.tag == "ADWare")
                    {
                        hit.transform.gameObject.GetComponentInParent<BaseAdware>().DuplicateEnemy();

                    }
                }
            }
        }
    }
}
