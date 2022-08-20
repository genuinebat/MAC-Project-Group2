using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        Coroutine cooldownCor, fillCor;

        float fill;
        bool breakCor = false;

        public void AdBlockerPressed()
        {
            AdblockerUI.SetActive(true);
            StartCoroutine(AdblockerUIFade());
            foreach (Transform child in SpawnnerHolder.transform)
            {
                GameObject.Destroy(child.gameObject);
                fillCor = StartCoroutine(ChangeFill());
                cooldownCor = StartCoroutine(ADBlockCooldown());
            }
        }

        IEnumerator AdblockerUIFade()
        {
            yield return new WaitForSeconds(2f);
            AdblockerUI.SetActive(false);
        }

        IEnumerator ADBlockCooldown()
        {
            GameObject.Find("AdBlockerBtn").GetComponent<Button>().interactable = false;

            // float elap = 0f;
            // while (elap < AdBlockerCooldownTime)
            // {
            //     if (breakCor) yield break;
            //     yield return null;
            // }
            yield return new WaitForSeconds(AdBlockerCooldownTime);

            GameObject.Find("AdBlockerBtn").GetComponent<Button>().interactable = true;

        }

        public void ResetCooldown()
        {
            StartCoroutine(StopEverything());

            GameObject.Find("AdBlockerBtn").GetComponent<Button>().interactable = true;

            Icon.fillAmount = 1f;
        }

        IEnumerator StopEverything()
        {
            breakCor = true;
            yield return new WaitForSeconds(.1f);
            breakCor = false;
        }

        IEnumerator ChangeFill()
        {
            float elap = 0f;

            while (elap < AdBlockerCooldownTime)
            {
                if (breakCor)
                {
                    yield break;
                }

                Icon.fillAmount = elap / AdBlockerCooldownTime;
                elap += Time.deltaTime;
                yield return null;
            }

            Icon.fillAmount = 1f;
        }
    }
}
