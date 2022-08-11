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
        public GameObject PauseUI;
        public GameObject SpawnStore;
        public GameObject TutorialPanel;
        public Button AntivirusBtn;

        [Header("Hint Text")]
        public string HintText;

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

            HintTxt.GetComponent<TMP_Text>().text = "hint: " + HintText;

            Cancel();
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

        // coroutine for the opening popup animation
        IEnumerator OpenPopup()
        {

            // expanding the popup horizontally
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

            // expanding the popup vertically
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

        // coroutine for the closing popup animation
        IEnumerator ClosePopup()
        {
            PopupDisplay.SetActive(false);

            // closing the popup vertically
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

            // closing the popup horizontally
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

            Popup.transform.localScale = new Vector3(0f, 0.1f, Popup.transform.localScale.z);

            spawner.GameStarted = true;

            TutorialPanel.SetActive(false);
            HintTxt.SetActive(false);
            UI.SetActive(true);
            PauseUI.SetActive(true);

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
            PauseUI.SetActive(false);

            timer.TimeLeft = timer.TimeMin * 60 + timer.TimeSec;
            foreach (Transform child in SpawnStore.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public void Retry()
        {
            StartCoroutine(RetryCor());
        }

        IEnumerator RetryCor()
        {
            Cancel();
            yield return new WaitForSeconds(.1f);
            Initialize();
        }

        public void OpenTutorialPanel()
        {
            TutorialPanel.SetActive(true);
            DisablePopup();
        }
    }
}