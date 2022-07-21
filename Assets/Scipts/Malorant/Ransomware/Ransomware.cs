using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Malorant
{
    public class Ransomware : MonoBehaviour, IDamageable
    {
        [Header("Function Variables")]
        public float Speed;

        GameObject scannerNotification; 
        GameObject lockedIcon;
        GameObject lockUI;
        GameObject unlockUI;
        Transform imageTarget;
        Button scanner;
        Image scannerIcon;
        Malorant_Spawner spawnnerScript;

        Vector3 targetLocation;
        float minX, maxX, minY, maxY, minZ, maxZ;

        // Start is called before the first frame update
        void Start()
        {
            scanner = GameObject.Find("Scanner").GetComponent<Button>();
            scannerIcon = GameObject.Find("ScannerImg").GetComponent<Image>();
            spawnnerScript = GameObject.Find("Spawner").GetComponent<Malorant_Spawner>();
            scannerNotification = GameObject.Find("ScannerUnlockUI");
            lockUI = GameObject.Find("Locked");
            unlockUI = GameObject.Find("Unlocked");
            imageTarget = GameObject.Find("Target").transform;

            scannerNotification.SetActive(false);
            unlockUI.SetActive(false);
            lockUI.SetActive(true);

            targetLocation = transform.position;
            StartCoroutine(PeriodicallySetBoundaries());
        }

        void Update()
        {
            if (Vector3.Distance(transform.position, targetLocation) < 0.01f)
            {
                SetNewRandomTargetLocation();
            }
            else
            {
                MoveToTargetLocation();
            }
        }

        public void GetHit()
        {

            scanner.interactable = true;
            scannerIcon.color=new Color32(255, 255, 255, 255);

            StartCoroutine(Unlock());
            StartCoroutine(ScannerNoti());

            spawnnerScript.SpawnMalwares2();
            
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }

        void SetBoundaries()
        {
            minX = imageTarget.position.x - 5;
            maxX = imageTarget.position.x + 5;
            minY = imageTarget.position.y - 5;
            maxY = imageTarget.position.y + 5;
            minZ = imageTarget.position.z - 3;
            maxZ = imageTarget.position.z;
        }

        IEnumerator PeriodicallySetBoundaries()
        {
            for (;;)
            {
                SetBoundaries();
                yield return new WaitForSeconds(.5f);
            }
        }

        void MoveToTargetLocation()
        {
            transform.position =
                Vector3.MoveTowards(
                    transform.position,
                    targetLocation,
                    Speed * Time.deltaTime
                );
        }

        void SetNewRandomTargetLocation()
        {
            targetLocation =
                new Vector3(
                    Random.Range(minX, maxX),
                    Random.Range(minY, maxY),
                    Random.Range(minZ, maxZ)
                );
        }

        IEnumerator ScannerNoti()
        {
            scannerNotification.SetActive(true);
            scannerNotification.GetComponent<Image>().color = new Color(1, 1, 1, 255);

            yield return new WaitForSeconds(1.5f);

            // loop over 1 second backwards
            for (float i = 1f; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                scannerNotification.GetComponent<Image>().color = new Color(1, 1, 1, i);
                
                yield return null;
            }
            scannerNotification.SetActive(false);

            Destroy(gameObject);

        }

        IEnumerator Unlock()
        {
            lockUI.SetActive(false);
            unlockUI.SetActive(true);

            RectTransform rt = unlockUI.GetComponent<RectTransform>();

            Vector2 ogPos = rt.position;
            Vector2 targetPos = ogPos + new Vector2(0, 200f);

            for (float i = 1.5f; i >= 0; i -= Time.deltaTime)
            {
                unlockUI.GetComponent<Image>().color = new Color(1, 1, 1, i);

                rt.position = Vector2.MoveTowards(rt.position, targetPos, Time.deltaTime * 100);

                yield return null;
            }

            unlockUI.SetActive(false);

            rt.position = ogPos;
            unlockUI.GetComponent<Image>().color = new Color(1, 1, 1, 255);
        }
    }
}

