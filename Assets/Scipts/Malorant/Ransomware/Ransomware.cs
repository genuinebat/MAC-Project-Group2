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
        Dialogue dialogueScript;
        Animator anim;

        Vector3 targetLocation;
        float minX, maxX, minY, maxY, minZ, maxZ;
        bool move;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(GameObject.Find("Scanner"));
            scannerIcon = GameObject.Find("ScannerImg").GetComponent<Image>();
            scanner = GameObject.Find("Scanner").GetComponent<Button>();
            spawnnerScript = GameObject.Find("Spawner").GetComponent<Malorant_Spawner>();
            scannerNotification = GameObject.Find("ScannerUnlockUI");
            lockUI = GameObject.Find("Locked");
            unlockUI = GameObject.Find("Unlocked");
            imageTarget = GameObject.Find("Target").transform;
            anim = GetComponent<Animator>();

            scannerNotification.SetActive(false);
            unlockUI.SetActive(false);
            lockUI.SetActive(true);

            move = true;

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
            spawnnerScript.SpawnMalwares2();

            scanner.interactable = true;
            scannerIcon.color = new Color32(255, 255, 255, 255);

            StartCoroutine(OpenLid());
            StartCoroutine(Unlock());
            StartCoroutine(ScannerNoti());
            
            gameObject.GetComponent<BoxCollider>().enabled = false;

            GameObject.Find("Malorant").GetComponent<Dialogue>().NextPhase = true;
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
            if (move)
            {
                transform.position =
                Vector3.MoveTowards(
                    transform.position,
                    targetLocation,
                    Speed * Time.deltaTime
                );
            }
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

        IEnumerator OpenLid()
        {
            move = false;
            anim.SetTrigger("OpenLid");
                
            yield return new WaitForSeconds(1f);

            GameObject top = transform.Find("Top").gameObject;
            GameObject bot = transform.Find("Base").gameObject;
            GameObject loc = transform.Find("LockPivot").Find("Lock").gameObject;

            top.GetComponent<MeshRenderer>().enabled = false;
            bot.GetComponent<MeshRenderer>().enabled = false;
            loc.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}

