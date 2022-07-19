using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Malorant
{
    public class MalorantSM : PuzzleSM
    {
        public GameObject Spawner, UI, Popup, PopupDisplay;

        Malorant_Spawner spawnerScript;
        Coroutine closingCor;

        Button scannerUI;
        Image scannerIcon;
        public MalorantGameState malorantState;
        public GameObject loseUI;

        float popupHeight, popupWidth;

        void Start()
        {
            spawnerScript = Spawner.GetComponent<Malorant_Spawner>();

            popupHeight = Popup.transform.localScale.y;
            popupWidth = Popup.transform.localScale.x;
            scannerUI = GameObject.Find("Scanner").GetComponent<Button>();
            scannerIcon = GameObject.Find("ScannerImg").GetComponent<Image>();
            Cancel();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) Initialize();
        }

        public override void EnablePopup()
        {
            if (IsRunning || IsCompleted) return;

            base.EnablePopup();

            if (closingCor != null) StopCoroutine(closingCor);

            Popup.transform.localScale = new Vector3(0f, 0.1f, Popup.transform.localScale.z);
            PopupDisplay.SetActive(false);
            Popup.SetActive(true);
            StartCoroutine(OpenPopup());
        }

        public override void DisablePopup()
        {
            closingCor = StartCoroutine(ClosePopup());
        }

        IEnumerator OpenPopup()
        {
            while (Popup.transform.localScale.x < (popupWidth - 0.02f))
            {
                Popup.transform.localScale = 
                    Vector3.Lerp(
                        Popup.transform.localScale, 
                        new Vector3(
                            popupWidth,
                            Popup.transform.localScale.y,
                            Popup.transform.localScale.z
                        ), 
                        5 * Time.deltaTime
                    );
                
                yield return null;
            }
            Popup.transform.localScale = new Vector3(popupWidth, Popup.transform.localScale.y, Popup.transform.localScale.z);

            yield return new WaitForSeconds(.1f);

            while (Popup.transform.localScale.y < (popupHeight - 0.02f))
            {
                Popup.transform.localScale = 
                    Vector3.Lerp(
                        Popup.transform.localScale, 
                        new Vector3(
                            popupWidth,
                            popupHeight,
                            Popup.transform.localScale.z
                        ), 
                        5 * Time.deltaTime
                    );
                
                yield return null;
            }
            Popup.transform.localScale = new Vector3(popupWidth, popupHeight, Popup.transform.localScale.z);
            
            PopupDisplay.SetActive(true);
        }

        IEnumerator ClosePopup()
        {
            PopupDisplay.SetActive(false);

            while (Popup.transform.localScale.y > (0.1f + 0.02f))
            {
                Popup.transform.localScale = 
                    Vector3.Lerp(
                        Popup.transform.localScale, 
                        new Vector3(
                            Popup.transform.localScale.x,
                            0.1f,
                            Popup.transform.localScale.z
                        ), 
                        5 * Time.deltaTime
                    );
                
                yield return null;
            }
            Popup.transform.localScale = new Vector3(Popup.transform.localScale.x, 0.1f, Popup.transform.localScale.z);
            
            yield return new WaitForSeconds(.1f);

            while (Popup.transform.localScale.x > 0.02f)
            {
                Popup.transform.localScale = 
                    Vector3.Lerp(
                        Popup.transform.localScale, 
                        new Vector3(
                            0f,
                            0.1f,
                            Popup.transform.localScale.z
                        ), 
                        5 * Time.deltaTime
                    );
                
                yield return null;
            }
            Popup.transform.localScale = new Vector3(0f, 0.1f, Popup.transform.localScale.z);

            closingCor = null;
            
            Popup.SetActive(false);
        }

        public override void Initialize()
        {
            if (IsRunning) return;

            base.Initialize();
            Popup.SetActive(false);
            Popup.transform.localScale = new Vector3(0f, 0.1f, Popup.transform.localScale.z);

            UI.SetActive(true);
            Spawner.SetActive(true);

            spawnerScript.SpawnMalwares();
        }

        public override void Cancel()
        {
            base.Cancel();
            //reset timer for malorant
            malorantState.timeLeft = malorantState.timeMin * 60 + malorantState.timeSec;
            Debug.Log(malorantState.timeMin);
            Debug.Log(malorantState.timeSec);
            Time.timeScale = 1f;
            Debug.Log(loseUI);
            UI.SetActive(false);

            //lock the scanner
            scannerUI.interactable = false;
            scannerIcon.color = new Color32(255, 255, 255, 0);
            //make sure lose UI is closed
            loseUI.SetActive(false);
            spawnerScript.ResetMalorant();
        }
    }
}