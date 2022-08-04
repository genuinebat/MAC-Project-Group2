using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WhackaAd
{
    public class WhackaAdSM : PuzzleSM
    {
        [Header("Reference Variables")]
        public GameObject Popup;
        public GameObject PopupDisplay;
        public GameObject TutorialPanelDisplay;
        public GameObject HintTxt;
        public GameObject UI;
        public GameObject LoseUI;
        public GameObject SpawnStore;
        public GameObject TutorialPanel;
        public Button AntivirusBtn;

        Coroutine closingCor;
        WhackaAdSpawner spawner;
        WhackaAdTime timer;

        float popupHeight, popupWidth;

        void Start()
        {
            spawner = transform.Find("Spawner").gameObject.GetComponent<WhackaAdSpawner>();
            timer = GetComponent<WhackaAdTime>();

            popupHeight = Popup.transform.localScale.y;
            popupWidth = Popup.transform.localScale.x;

            HintTxt.GetComponent<TMP_Text>().text = "hint: my wall is on fire lol";

            Cancel();
        }

        public override void EnablePopup(GameObject PopUpTemp, GameObject DisplayTemp)
        {
            Debug.Log("called");
            if (IsRunning || IsCompleted) return;

            base.EnablePopup();

            Debug.Log("got thru");

            if (closingCor != null) StopCoroutine(closingCor);

            PopUpTemp.transform.localScale = new Vector3(0f, 0.1f, PopUpTemp.transform.localScale.z);

            DisplayTemp.SetActive(false);
            PopUpTemp.SetActive(true);

            StartCoroutine(OpenPopup(PopUpTemp, DisplayTemp));
        }

        public override void DisablePopup(GameObject PopUpTemp, GameObject DisplayTemp)
        {
            closingCor = StartCoroutine(ClosePopup(PopUpTemp, DisplayTemp));
        }

        // coroutine for the opening popup animation
        IEnumerator OpenPopup(GameObject PopUpTemp, GameObject DisplayTemp)
        {

            // expanding the popup horizontally
            while (PopUpTemp.transform.localScale.x < (popupWidth - 0.02f))
            {
                Debug.Log("running");
                PopUpTemp.transform.localScale =
                    Vector3.Lerp(
                        PopUpTemp.transform.localScale,
                        new Vector3(
                            popupWidth,
                            PopUpTemp.transform.localScale.y,
                            PopUpTemp.transform.localScale.z
                        ),
                        5 * Time.deltaTime
                    );

                yield return null;
            }
            PopUpTemp.transform.localScale = new Vector3(popupWidth, PopUpTemp.transform.localScale.y, Popup.transform.localScale.z);

            yield return new WaitForSeconds(.1f);

            // expanding the popup vertically
            while (PopUpTemp.transform.localScale.y < (popupHeight - 0.02f))
            {
                PopUpTemp.transform.localScale =
                    Vector3.Lerp(
                        PopUpTemp.transform.localScale,
                        new Vector3(
                            popupWidth,
                            popupHeight,
                            PopUpTemp.transform.localScale.z
                        ),
                        5 * Time.deltaTime
                    );

                yield return null;
            }
            PopUpTemp.transform.localScale = new Vector3(popupWidth, popupHeight, PopUpTemp.transform.localScale.z);

            DisplayTemp.SetActive(true);
        }

        // coroutine for the closing popup animation
        IEnumerator ClosePopup(GameObject PopUpTemp, GameObject DisplayTemp)
        {
            DisplayTemp.SetActive(false);

            // closing the popup vertically
            while (Popup.transform.localScale.y > (0.1f + 0.02f))
            {
                PopUpTemp.transform.localScale =
                    Vector3.Lerp(
                        PopUpTemp.transform.localScale,
                        new Vector3(
                            PopUpTemp.transform.localScale.x,
                            0.1f,
                            PopUpTemp.transform.localScale.z
                        ),
                        5 * Time.deltaTime
                    );

                yield return null;
            }
            PopUpTemp.transform.localScale = new Vector3(Popup.transform.localScale.x, 0.1f, PopUpTemp.transform.localScale.z);

            yield return new WaitForSeconds(.1f);

            // closing the popup horizontally
            while (PopUpTemp.transform.localScale.x > 0.02f)
            {
                PopUpTemp.transform.localScale =
                    Vector3.Lerp(
                        PopUpTemp.transform.localScale,
                        new Vector3(
                            0f,
                            0.1f,
                            PopUpTemp.transform.localScale.z
                        ),
                        5 * Time.deltaTime
                    );

                yield return null;
            }
            PopUpTemp.transform.localScale = new Vector3(0f, 0.1f, PopUpTemp.transform.localScale.z);

            closingCor = null;

            PopUpTemp.SetActive(false);
        }

        public override void Initialize()
        {
            DisablePopup(TutorialPanel, TutorialPanelDisplay);
            if (IsRunning) return;

            base.Initialize();

            PopUpTemp.transform.localScale = new Vector3(0f, 0.1f, PopUpTemp.transform.localScale.z);

            spawner.GameStarted = true;

            HintTxt.SetActive(false);
            UI.SetActive(true);

            timer.GameStarted = true;
        }

        public override void Cancel()
        {
            base.Cancel();

            Time.timeScale = 1f;

            UI.SetActive(false);
            LoseUI.SetActive(false);
            timer.GameStarted = false;
            spawner.GameStarted = false;
            AntivirusBtn.enabled = true;
            HintTxt.SetActive(true);

            spawner.AdwareTemp.AddRange(spawner.Adwares);

            timer.TimeLeft = timer.TimeMin * 60 + timer.TimeSec;
            foreach (Transform child in SpawnStore.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }


        public void OpenTutorialPanel()
        {
            TutorialPanel.SetActive(true);
            EnablePopup(TutorialPanel, TutorialPanelDisplay);
            DisablePopup(Popup, PopupDisplay);
        }
    }
}