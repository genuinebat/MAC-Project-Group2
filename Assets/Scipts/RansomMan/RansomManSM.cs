using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Pathfinding;

namespace RansomMan
{
    public class RansomManSM : PuzzleSM
    {
        [Header("Reference Variables")]
        public GameObject Popup;
        public GameObject PopupDisplay;
        public GameObject HintTxt;
        public GameObject HintTxtBack;
        public GameObject UI;
        public GameObject WinUI;
        public GameObject LoseUI;
        public Button PauseRetry;
        public GameObject Player;
        public GameObject Back;
        public GameObject ARCam;
        public GameObject Cam;
        public GameObject TutorialPanel;

        public Button SkipButton;
        public RansomwareSpawner Spawner;
        public NodeManager nm;

        [Header("Hint Text")]
        public string HintText;
        int tryCount;


        Coroutine closingCor;
        ByteTracker bt;

        float popupHeight, popupWidth;

        void Start()
        {
            bt = GetComponent<ByteTracker>();

            popupHeight = Popup.transform.localScale.y;
            popupWidth = Popup.transform.localScale.x;

            HintTxt.GetComponent<TMP_Text>().text = "hint: " + HintText;

            Cancel();

            // FOR DEVLOPMENT ONLY
            //StartCoroutine(LateStart());
        }

        void Update()
        {
            PauseRetry.interactable = IsRunning;
        }

        // FOR DEVELOPMENT ONLY
        IEnumerator LateStart()
        {
            yield return new WaitForSeconds(3f);
            Initialize();
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

            tryCount++;

            if (tryCount >= 2)
            {
                StartCoroutine(DelaySkipBtn());
            }

            TutorialPanel.SetActive(false);
            ARCam.SetActive(false);
            Cam.SetActive(true);

            Popup.transform.localScale = new Vector3(0f, 0.1f, Popup.transform.localScale.z);

            HintTxt.SetActive(false);
            HintTxtBack.SetActive(false);
            UI.SetActive(true);

            nm.SpawnMapPrefabs();

            PlayerMovement pm = Player.GetComponent<PlayerMovement>();

            pm.PlayerStartPosition();
            pm.GameStarted = true;

            Spawner.SpawnRansomwares();

            bt.Collected = 0;
            bt.GameStarted = true;
        }

        IEnumerator DelaySkipBtn()
        {
            yield return new WaitForSeconds(.5f);
            SkipButton.interactable = true;
        }

        public override void Cancel()
        {
            base.Cancel();

            Time.timeScale = 1f;

            Player.GetComponent<RansomManCollector>().StopAllCoroutines();

            UI.SetActive(false);
            HintTxt.SetActive(true);
            HintTxtBack.SetActive(true);
            WinUI.SetActive(false);
            LoseUI.SetActive(false);

            Player.transform.position = new Vector3(100, 100, 100);

            // reset nodemanager, ransomwares, player and turn off all gameobject associated with it
            foreach (Transform child in Spawner.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in nm.transform)
            {
                Destroy(child.gameObject);
            }

            Back.SetActive(false);
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
        public void SkipGame(string TargetSceneName)
        {
            Time.timeScale = 1f;
            PlayerPrefs.SetInt("Puzzle4", 1);

            if (tryCount >= 2)
            {
                GameObject.Find("TransitionAnimator").GetComponent<ChangeScene>().nextScene(TargetSceneName);

                if (PlayerPrefs.GetString("NextStage") != "Completed")
                {
                    PlayerPrefs.SetString("NextStage", TargetSceneName);
                }
            }
        }
        public void OpenTutorialPanel()
        {
            TutorialPanel.SetActive(true);
            DisablePopup();
        }

        //this is referenced in Ransomware because LoseUI gameobject cant be aassigned if it is not active
        public void LoseGameUIActive()
        {
            Time.timeScale = 0f;
            LoseUI.SetActive(true);
        }
    }
}