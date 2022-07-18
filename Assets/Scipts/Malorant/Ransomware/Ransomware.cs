using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Malorant
{
    public class Ransomware : MonoBehaviour, IDamageable
    {
        private GameObject lockedScanner;
        private Button scanner;
        private Image scannerIcon;
        public float Speed;
        Transform imageTarget;
        Vector3 targetLocation;
        float minX, maxX, minY, maxY, minZ, maxZ;

        // Start is called before the first frame update
        void Start()
        {
            imageTarget = GameObject.Find("Target").transform;
            scanner = GameObject.Find("Scanner").GetComponent<Button>();
            scannerIcon = GameObject.Find("ScannerImg").GetComponent<Image>();

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
            scannerIcon.color=new Color32(255, 255, 255, 250);
            //lockedScanner.SetActive(false);
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
    }
}

