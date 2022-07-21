using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Malorant
{
    public class Ransomware : MonoBehaviour, IDamageable
    {
        public float Speed;

        GameObject lockedIcon;
        Button scanner;
        Image scannerIcon;
        Malorant_Spawner spawnnerScript;
        GameObject scannerNotification;     
        Transform imageTarget;
        Vector3 targetLocation;
        float minX, maxX, minY, maxY, minZ, maxZ;

        // Start is called before the first frame update
        void Start()
        {
            scanner = GameObject.Find("Scanner").GetComponent<Button>();
            scannerIcon = GameObject.Find("ScannerImg").GetComponent<Image>();
            spawnnerScript = GameObject.Find("Spawner").GetComponent<Malorant_Spawner>();
            scannerNotification = GameObject.Find("ScannerUnlockUI");
            imageTarget = GameObject.Find("Target").transform;

            scannerNotification.SetActive(false);
            targetLocation = transform.position;
            StartCoroutine(PeriodicallySetBoundaries());

        }

        // Update is called once per frame
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
            spawnnerScript.SpawnMalwares2();
            StartCoroutine(scannerNoti());
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

        IEnumerator scannerNoti()
        {
            scannerNotification.SetActive(true);
            scannerNotification.GetComponent<Image>().color = new Color(1, 1, 1, 255);
            yield return new WaitForSeconds(2);
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                scannerNotification.GetComponent<Image>().color = new Color(1, 1, 1, i);
                yield return null;
            }
            scannerNotification.SetActive(false);

            Destroy(gameObject);

        }
    }
}

