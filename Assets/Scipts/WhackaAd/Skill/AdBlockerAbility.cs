using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WhackaAd
{
    public class AdBlockerAbility : MonoBehaviour
    {
        [Header("Reference Variables")]
        public GameObject SpawnnerHolder;

        public Image Icon;

        public float AdBlockerCooldownTime;

        public GameObject AdblockerUI;

        public GameObject AdblockUIText;

        float fill;

        void Start()
        {
        }

        void Update()
        {
        }

        public void AdBlockerPressed()
        {
            foreach (Transform child in SpawnnerHolder.transform)
            {
                GameObject.Destroy(child.gameObject);
                StartCoroutine(ChangeFill());
                StartCoroutine(ADBlockCooldown());
            }
        }

        IEnumerator ADBlockCooldown()
        {
            GameObject.Find("AdBlockerBtn").GetComponent<Button>().enabled =
                false;
            yield return new WaitForSeconds(AdBlockerCooldownTime);
            GameObject.Find("AdBlockerBtn").GetComponent<Button>().enabled =
                true;
        }

        IEnumerator ChangeFill()
        {
            float elap = 0f;

            while (elap < AdBlockerCooldownTime)
            {
                Icon.fillAmount = elap / AdBlockerCooldownTime;
                elap += Time.deltaTime;
                yield return null;
            }

            Icon.fillAmount = 1f;
        }

        public void AdblockerUIActive()
        {
            AdblockerUI.SetActive(true);
            AdblockerUI.Image.SetColor(255, 255, 255, 255);
            AdblockUIText.SetColor(255, 255, 255, 255);
        }
    }
}
