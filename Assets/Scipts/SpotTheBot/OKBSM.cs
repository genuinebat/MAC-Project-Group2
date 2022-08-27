using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace OKB
{
    public class OKBSM : PuzzleSM
    {
        [Header("Reference Varaibles")]
        public GameObject Popup;
        public GameObject PopupDisplay;
        public GameObject UI;
        public GameObject HintTxt;
        public GameObject HintTxtBack;
        public GameObject WinUI;
        public GameObject LoseUI;
        public GameObject TrojanLoseUI;
        public Button PauseRetry;
        public GameObject ARCam;
        public GameObject GameCam;
        public GameObject TutorialUI;
        public Button SkipButton;
        public OKBTimer TimerScript;

        [Header("Hint Text")]
        public string HintText;
        [HideInInspector]
        public int tryCount;

        TemplateManager tm;
        Swipe sw;

        Coroutine closingCor;

        float popupHeight, popupWidth;

        void Start()
        {
            tm = GetComponent<TemplateManager>();
            sw = GetComponent<Swipe>();

            popupHeight = Popup.transform.localScale.y;
            popupWidth = Popup.transform.localScale.x;

            HintTxt.GetComponent<TMP_Text>().text = "hint: " + HintText;

            Cancel();
            //StartCoroutine(DelayStart());
        }

        void Update()
        {
            PauseRetry.interactable = IsRunning;

        }

        IEnumerator DelayStart()
        {
            yield return new WaitForSeconds(1f);
            OpenTutorial();
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

            TimerScript.timeLeft = TimerScript.TimeMin * 60 + TimerScript.TimeSec;
            TutorialUI.SetActive(false);
            Time.timeScale = 1;

            tryCount++;

            if (tryCount >= 2)
            {
                SkipButton.interactable = true;
            }

            Popup.transform.localScale = new Vector3(0f, 0.1f, Popup.transform.localScale.z);

            HintTxt.SetActive(false);
            HintTxtBack.SetActive(false);
            UI.SetActive(true);

            ARCam.SetActive(false);
            GameCam.SetActive(true);

            sw.Trojan = false;

            tm.Bot.transform.localPosition = new Vector3(0f, -1.4f, -0.4f);
            tm.Troy.transform.localPosition = new Vector3(0, -1.3f, -0.2f);

            tm.NewGameFunc();
            tm.SetNewBot();

            TimerScript.GameStart = true;
        }

        public override void Cancel()
        {
            base.Cancel();

            Time.timeScale = 1f;

            tm.Bot.SetActive(true);
            tm.Troy.SetActive(false);

            UI.SetActive(false);
            HintTxtBack.SetActive(true);
            HintTxt.SetActive(true);
            WinUI.SetActive(false);
            LoseUI.SetActive(false);
            TrojanLoseUI.SetActive(false);

            sw.Active = false;
        }

        public void Retry()
        {
            TimerScript.timeLeft = TimerScript.TimeMin * 60 + TimerScript.TimeSec;
            StartCoroutine(RetryCor());
        }

        public void OpenTutorial()
        {
            DisablePopup();
            TutorialUI.SetActive(true);
        }

        IEnumerator RetryCor()
        {
            LoseUI.SetActive(false);
            Cancel();
            yield return new WaitForSeconds(.1f);
            Initialize();
        }

        public void SkipGame(string TargetSceneName)
        {
            if (tryCount >= 2)
            {
                GameObject.Find("TransitionAnimator").GetComponent<ChangeScene>().nextScene(TargetSceneName);
            }
        }
    }
}