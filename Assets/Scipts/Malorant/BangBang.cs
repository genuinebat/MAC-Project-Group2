using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class BangBang : MonoBehaviour
    {
        public GameObject hitEffect;
        private bool delayRunning;

        public void Bang()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(hitEffect, hit.transform.position, Quaternion.identity);
                StartCoroutine(DelayShot(hit));
            }
        }

        IEnumerator DelayShot(RaycastHit hit)
        {
            yield return new WaitForSeconds(0.5f);

            if (hit.transform.tag == "Enemy")
            {
                // FPSEnemy enemy = hit.transform.gameObject.GetComponent<FPSEnemy>();

                // if (enemy.Type == weaponEquipped.Target)
                // {
                //     enemy.TakeDamage(true);
                // }
                // else
                // {
                //     enemy.TakeDamage(false);
                // }
            }
        }
    }
}
