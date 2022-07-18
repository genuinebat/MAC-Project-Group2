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
        public GameObject RaygunUI, ScannerUI, RaygunCrosshair, ScannerCrosshair, TargetFound, Trigger;

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
        }

        void WithinScan()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "ScannableEnemy")
                {
                    TargetFound.SetActive(true);
                    TargetFoundTxt.text = "???";
                    scannable = true;
                    scannableObj = hit.transform.gameObject;

                    return;
                }

                if (hit.transform.tag == "Enemy")
                {
                    TargetFound.SetActive(true);
                    TargetFoundTxt.text = hit.transform.name.Replace("(Clone)", "");
                    scannable = false;

                    return;
                }
            }

            TargetFound.SetActive(false);
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
            TargetFound.SetActive(false);
            TargetFoundTxt.text = "No Target Found";

            Trigger.GetComponent<Image>().color = Color.yellow;

            ScannerFill.fillAmount = 0f;

            shootBtn.onClick.RemoveListener(RaygunOnClick);
        }
    }
}