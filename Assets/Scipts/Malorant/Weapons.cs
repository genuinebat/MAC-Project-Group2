using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Malorant
{
    interface IDamageable
    {
        void GetHit();
    }

    interface IScannable
    {
        bool Scanned { get; }
        void Scan();
    }
    
    enum Gun {Raygun, Scanner}

    public class Weapons : MonoBehaviour
    {
        public GameObject RaygunUI, ScannerUI, RaygunCrosshair, ScannerCrosshair, Trigger, ScannerFillBack, ScannedTxt;

        public Image ScannerFill;
        public TextMeshProUGUI TargetFoundTxt;

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
            pressShootScript = shootBtn.gameObject.GetComponent<PressingButton>();

            ScannedTxt.SetActive(false);

            equipped = Gun.Raygun;
            SwitchToRaygun();
        }

        void Update()
        {
            if (equipped == Gun.Scanner)
            {
                WithinScan();
                if (scannable)
                {
                    if (pressShootScript.Pressing)
                    {
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

        void RaygunOnClick()
        {
            bangScript.Bang();
        }

        void CompleteScan()
        {
            ScannerFill.fillAmount = 0f;
            elap = 0f;

            scannableObj.GetComponent<IScannable>().Scan();

            StartCoroutine(ScannedText());
        }

        void WithinScan()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
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

            ScannerFillBack.SetActive(false);
            TargetFoundTxt.text = "No Target Found";
            scannable = false;
        }

        public void SwitchToRaygun()
        {
            equipped = Gun.Raygun;

            RaygunUI.SetActive(true);
            ScannerUI.SetActive(false);
            RaygunCrosshair.SetActive(true);
            ScannerCrosshair.SetActive(false);
            ScannerFillBack.SetActive(false);

            Trigger.GetComponent<Image>().color = Color.red;

            shootBtn.onClick.RemoveListener(RaygunOnClick);

            shootBtn.onClick.AddListener(RaygunOnClick);
        }

        public void SwitchToScanner()
        {
            equipped = Gun.Scanner;

            RaygunUI.SetActive(false);
            ScannerUI.SetActive(true);
            RaygunCrosshair.SetActive(false);
            ScannerCrosshair.SetActive(true);

            TargetFoundTxt.text = "No Target Found";

            Trigger.GetComponent<Image>().color = Color.yellow;

            ScannerFill.fillAmount = 0f;

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