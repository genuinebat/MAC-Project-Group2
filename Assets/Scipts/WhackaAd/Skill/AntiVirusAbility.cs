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
        public float AntiVirusCooldownTime;
        public Button AntivirusBtn;

        int removed;

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
            Time.timeScale = 0.2f;
            if (Spawner.AdwareTemp.Count == 1)
            {
                Spawner.AdwareTemp.Add(Spawner.Adwares[removed]);
                Spawner.AdwareTemp.RemoveAt(0);
            }
            else if (Spawner.AdwareTemp.Count > 1)
            {
                Spawner.AdwareTemp.RemoveAt(index);
            }
            removed = index;
            AntivirusUI.SetActive(false);
            Time.timeScale = 1f;
            StartCoroutine(ChangeFill());
            StartCoroutine(AntiVirusCooldown());
        }

        IEnumerator AntiVirusCooldown()
        {
            AntivirusBtn.enabled = false;
            yield return new WaitForSeconds(AntiVirusCooldownTime);
            AntivirusBtn.enabled = true;
        }

        IEnumerator ChangeFill()
        {
            float elap = 0f;

            while (elap < AntiVirusCooldownTime)
            {
                Icon.fillAmount = elap / AntiVirusCooldownTime;
                elap += Time.deltaTime;
                yield return null;
            }

            Icon.fillAmount = 1f;
        }
    }
}