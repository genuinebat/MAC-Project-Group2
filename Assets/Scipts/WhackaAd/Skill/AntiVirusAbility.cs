using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WhackaAd
{
    public class AntiVirusAbility : MonoBehaviour
    {
        public WhackaAdSpawner Spawner;
        public GameObject AntivirusUI;
        public Image Icon;
        public float AntiVirusDuration;
        public float AntiVirusCooldownTime;
        public Button AntivirusBtn;

        bool breakCor = false;

        public void AntiVirusMenu()
        {
            AntivirusUI.SetActive(true);
        }

        public void CloseAntiVirusMenu()
        {
            AntivirusUI.SetActive(false);
        }

        public void AntiVirus(int index)
        {
            AntivirusUI.SetActive(false);
            StartCoroutine(AntiVirusActivate(index));
            StartCoroutine(ChangeFill());
            StartCoroutine(AntiVirusCooldown());
        }

        IEnumerator AntiVirusActivate(int index)
        {
            Spawner.AdwareTemp.RemoveAt(index);
            yield return new WaitForSeconds(AntiVirusDuration);
            Spawner.AdwareTemp.Clear();
            Spawner.AdwareTemp.AddRange(Spawner.Adwares);
        }

        IEnumerator AntiVirusCooldown()
        {
            AntivirusBtn.enabled = false;

            float elap = 0f;
            while (elap < AntiVirusCooldownTime)
            {
                if (breakCor) yield break;
                yield return null;
            }
            AntivirusBtn.enabled = true;
        }

        public void ResetCooldown()
        {
            StartCoroutine(StopEverything());

            AntivirusBtn.enabled = true;
            Icon.fillAmount = 1f;
            Spawner.AdwareTemp.Clear();
            Spawner.AdwareTemp.AddRange(Spawner.Adwares);
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

            while (elap < AntiVirusCooldownTime)
            {
                if (breakCor) yield break;

                Icon.fillAmount = elap / AntiVirusCooldownTime;
                elap += Time.deltaTime;
                yield return null;
            }

            Icon.fillAmount = 1f;
        }
    }
}