using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Malorant
{
    public enum Gun {Raygun, Scanner}

    public class Weapons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public GameObject RaygunUI, ScannerUI, RaygunCrosshair, ScannerCrosshair;

        public Button ShootBtn;
        public Image ScannerFill;

        public float ScanDuration;

        BangBang bangScript;
        Gun equipped;

        bool pressingShoot;
        float elap;

        void Start()
        {
            bangScript = GetComponent<BangBang>();

            equipped = Gun.Raygun;
            pressingShoot = false;
            SwitchToRaygun();
        }

        void Update()
        {
            if (equipped == Gun.Scanner)
            {
                if (pressingShoot)
                {
                    if (WithinScan())
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
                    }
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
        }

        bool WithinScan()
        {
            return true;
        }

        public void OnPointerDown(PointerEventData eventData){
            pressingShoot = true;
        }
        
        public void OnPointerUp(PointerEventData eventData){
            pressingShoot = false;
            elap = 0f;
        }

        public void SwitchToRaygun()
        {
            equipped = Gun.Raygun;
            RaygunUI.SetActive(true);
            ScannerUI.SetActive(false);
            RaygunCrosshair.SetActive(true);
            ScannerCrosshair.SetActive(false);

            ShootBtn.onClick.RemoveListener(RaygunOnClick);

            ShootBtn.onClick.AddListener(RaygunOnClick);
        }

        public void SwitchToScanner()
        {
            equipped = Gun.Scanner;
            RaygunUI.SetActive(false);
            ScannerUI.SetActive(true);
            RaygunCrosshair.SetActive(false);
            ScannerCrosshair.SetActive(true);

            ShootBtn.onClick.RemoveListener(RaygunOnClick);
        }
    }
}