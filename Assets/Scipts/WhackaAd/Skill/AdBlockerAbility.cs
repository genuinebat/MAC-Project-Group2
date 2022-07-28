using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WhackaAd
{
    public class AdBlockerAbility : MonoBehaviour
    {
        [Header("Reference Variables")]
        //public GameObject AdBlockderBtn;
        //public GameObject PreAimUI;
        public GameObject SpawnnerHolder;
        public float AdBlockerCooldownTime;

        void Start() { }

        void Update() { }

        public void AdBlockerPressed()
        {
            foreach (Transform child in SpawnnerHolder.transform)
            {
                GameObject.Destroy(child.gameObject);
                StartCoroutine(ADBlockCooldown());
            }
        }

        IEnumerator ADBlockCooldown()
        {
            GameObject.Find("AdBlockerBtn").GetComponent<Button>().enabled = false;
            yield return new WaitForSeconds(AdBlockerCooldownTime);
            GameObject.Find("AdBlockerBtn").GetComponent<Button>().enabled = true;
        }
    }
}
