using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Malorant
{
    // interface that all entities that are able to take damage will implement
    interface IDamageable
    {
        void GetHit();
    }

    // interface that all entities that are able to be scanned will implement
    interface IScannable
    {
        bool Scanned { get; }
        void Scan();
    }
    
    // enumerator of all types of guns
    enum Gun {Raygun, Scanner}

    public class Weapons : MonoBehaviour
    {
        [Header("Reference Variables")]
        public GameObject RaygunUI;
        public GameObject ScannerUI;
        public GameObject RaygunCrosshair;
        public GameObject ScannerCrosshair;
        public GameObject Trigger;
        public GameObject ScannerFillBack;
        public GameObject ScannedTxt;
        public Image ScannerFill;
        public TextMeshProUGUI TargetFoundTxt;

        [Header("Functionality Variables")]
        public float ScanDuration = 2;

        Button shootBtn;
        BangBang bangScript;
        PressingButton pressShootScript;

        Gun equipped;

        GameObject scannableObj;

        float elap;
        bool scannable;

        void Start()
        {
            shootBtn = Trigger.GetComponent<Button>();
            bangScript = GetComponent<BangBang>();
            pressShootScript = shootBtn.GetComponent<PressingButton>();

            ScannedTxt.SetActive(false);

            // equipping the raygun by default
            equipped = Gun.Raygun;
            SwitchToRaygun();
        }

        void Update()
        {
            if (equipped == Gun.Scanner)
            {
                // checking if an enitity is within the scanner's crosshair
                WithinScan();
                if (scannable)
                {
                    // checking if the shoot button is being held down
                    if (pressShootScript.Pressing)
                    {
                        // updating the scanner fill amount based on
                        // the required scan duration and the amount
                        // of time that has been spent scanning
                        ScannerFill.fillAmount = elap / ScanDuration;

                        if (elap >= ScanDuration)
                        {
                            CompleteScan();
                        }
                        else elap += Time.deltaTime;
                    }
                    else
                    {
                        ScannerFill.fillAmount = 0f;
                        elap = 0f;
                    }
                }
                else
                {
                    ScannerFill.fillAmount = 0f;
                    elap = 0f;
                }
            }
        }

        // function that executes when the raygun is equipped and shot
        void RaygunOnClick()
        {
            bangScript.Bang();
        }

        void WithinScan()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Trojan")
                {
                    ScannerFillBack.SetActive(true);
                    TargetFoundTxt.text = "!?Error~?";
                    TargetFoundTxt.color = Color.red;
                    scannable = false;

                    return;
                }

                if (hit.transform.tag == "ScannableEnemy")
                {
                    ScannerFillBack.SetActive(true);
                    TargetFoundTxt.text = "???";
                    scannable = true;
                    scannableObj = hit.transform.gameObject;

                    return;
                }

                if (hit.transform.tag == "Enemy")
                {
                    ScannerFillBack.SetActive(true);
                    TargetFoundTxt.text = hit.transform.name.Replace("(Clone)", "");
                    scannable = false;
                    
                    return;
                }
            }
            else
            {
                TargetFoundTxt.color = Color.white;
            }

            ScannerFillBack.SetActive(false);
            TargetFoundTxt.text = "No Target Found";
            scannable = false;
        }

        // function that calls once scanning is complete
        void CompleteScan()
        {
            ScannerFill.fillAmount = 0f;
            elap = 0f;

            scannableObj.GetComponent<IScannable>().Scan();

            StartCoroutine(ScannedText());
        }

        // function that calls when switching to the raygun
        public void SwitchToRaygun()
        {
            equipped = Gun.Raygun;

            RaygunUI.SetActive(true);
            ScannerUI.SetActive(false);
            RaygunCrosshair.SetActive(true);
            ScannerCrosshair.SetActive(false);
            ScannerFillBack.SetActive(false);

            // removing all instances of the respective listener function
            // then attaching 1 instance of it
            shootBtn.onClick.RemoveListener(RaygunOnClick);
            shootBtn.onClick.AddListener(RaygunOnClick);
        }

        // function that calls when switching to the scanner
        public void SwitchToScanner()
        {
            equipped = Gun.Scanner;

            RaygunUI.SetActive(false);
            ScannerUI.SetActive(true);
            RaygunCrosshair.SetActive(false);
            ScannerCrosshair.SetActive(true);

            TargetFoundTxt.text = "No Target Found";

            ScannerFill.fillAmount = 0f;

            // removing all instances of the other listener function
            shootBtn.onClick.RemoveListener(RaygunOnClick);
        }
        
        IEnumerator ScannedText()
        {            
            RectTransform rt = ScannedTxt.GetComponent<RectTransform>();

            Vector2 ogPos = rt.position;
            Vector3 ogScale = rt.localScale;
            
            Vector2 targetPos = ogPos + new Vector2(0f, 80f);
            Vector3 targetScale = ogScale * 0.6f;
            targetScale.z = 1;

            ScannedTxt.SetActive(true);

            while (Vector2.Distance(rt.position, targetPos) > 20f)
            {
                rt.position = Vector2.MoveTowards(rt.position, targetPos, Time.deltaTime * 60);

                rt.localScale = Vector3.Lerp(rt.localScale, targetScale, Time.deltaTime * 2);
                
                yield return null;
            }

            ScannedTxt.SetActive(false);
            
            rt.position = ogPos;
            rt.localScale = ogScale;
        }
    }
}